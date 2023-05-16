using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Socotra.VinLookup.Models;
using Microsoft.Extensions.Logging;
using Socotra.VinLookup.Logic;

namespace MainVinController.Controllers
{

    [ApiController]
    [Route("/vehicleLookup")]
    public class MainVinController : ControllerBase
    {
        private readonly ILogger<MainVinController> _logger;
        private readonly AutofillHelper _helper;
        private readonly StateApiHelper _stateApiHelper;
        private readonly VinApiHelper _vinApiHelper;
        private readonly Validator _validator;
        public MainVinController(ILogger<MainVinController> logger, AutofillHelper helper, Validator validator, StateApiHelper stateApiHelper, VinApiHelper vinApiHelper)
        {
            _logger = logger ?? throw new NullReferenceException("logger");
            _helper = helper ?? throw new NullReferenceException("helper");
            _stateApiHelper = stateApiHelper ?? throw new NullReferenceException("state api helper");
            _vinApiHelper = vinApiHelper ?? throw new NullReferenceException("vin api helper");
            _validator = validator ?? throw new NullReferenceException("validator");
        }

        private void log(string message) => _logger.LogInformation(message);

        private void logRequest<TObject>(TObject request, string message)
        {
            string incomingAutofillRequest = JsonSerializer.Serialize<TObject>(request);
            _logger.LogInformation(message);
            _logger.LogInformation(incomingAutofillRequest);
        }

        [HttpPost("")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(AutofillResponse), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> Index([FromBody] AutofillRequest request)
        {
            logRequest(request, "Incoming Autofill Request.");
            try
            {
                var xSmpKey = HttpContext.Request.Headers["x-smp-key"];
                var StateApiResp = await _stateApiHelper.FetchState(xSmpKey);

                logRequest(StateApiResp, "Incoming State Api Resp.");

                var resp = _helper.BuildInitialAutofillResponse(request);

                // map path shows us where values live in our socotra policy, the last value in path is the respective namespace
                string vinMappedPath = _stateApiHelper.GetMappingPath(StateApiResp, MappingValues.vin);
                string vinLocation = _stateApiHelper.FindPathType(vinMappedPath);
                string vinNameSpace = _stateApiHelper.GetFieldName(vinMappedPath);

                int numberOfVinFields = _vinApiHelper.CountAllVins(vinLocation, resp);
                var vins = new VinsInfo[numberOfVinFields];

                if (!(_validator.ValueIsPresent(vinLocation)))
                {
                    _logger.LogInformation("VinLocation length is 0, can't do anything");
                    return Ok(resp);
                }
                switch (vinLocation)
                {
                    case PolicyConstants.fieldType.isFieldValueField:
                        vins[0] = await _vinApiHelper.AddFieldValueVinInfo(request, vinNameSpace, vinLocation);
                        break;
                    case PolicyConstants.fieldType.isExposureField:
                        vins = await _vinApiHelper.AddExposuresVinInfo(request, vins, vinNameSpace, vinLocation);
                        break;
                    case PolicyConstants.fieldType.isFieldValueGroup:
                        string groupName = _stateApiHelper.getGroupName(vinMappedPath);
                        vins = await _vinApiHelper.AddFieldGroupVinInfo(request, vins, vinNameSpace, groupName, vinLocation);
                        break;
                }
                logRequest(vins[1], "Current Vin");
                logRequest(resp, "Resp updates");
                if (vins.Any())
                {
                    foreach (string fieldName in PossibleStateApiMappings.MappingValuesList)
                    {
                        resp = _helper.BuildAutoFillResponse(resp, StateApiResp, vins, fieldName);
                    }

                }
                logRequest(resp, "Return Autofill Resp.");

                return Ok(resp);
            }
            catch (Exception e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return Ok(e.Message);
            }
        }

        [HttpPost("tenpin")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(TenpinResponse), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> Tenpin([FromBody] TenpinRequest request)
        {
            try
            {
                TenpinResponse response = new();
                if (String.IsNullOrWhiteSpace(request.vin))
                {
                    throw new NullReferenceException("no vin provided");
                }
                CapturedVinFieldValues actualVinResp = await _vinApiHelper.RetrieveVIN(request.vin);

                if (actualVinResp is not null)
                {
                    response.make = actualVinResp.MapField(MappingValues.make);
                    response.model = actualVinResp.MapField(MappingValues.model);
                    response.year = actualVinResp.MapField(MappingValues.year);
                }

                return Ok(response);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return Ok(e.Message);
            }
        }
    }
}
