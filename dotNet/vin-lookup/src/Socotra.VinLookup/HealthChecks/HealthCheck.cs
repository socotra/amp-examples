using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthChecks;
	public class GeneralHealthChecks : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(HealthCheckResult.Healthy("Healthy!"));
    }
}