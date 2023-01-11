import { Body, Controller, Post, Req } from '@nestjs/common';
import { Request } from 'express';
import { AppService } from './app.service';
import { AutofillPayload, AutofillResponse } from './socotra/autofill';
import { State } from './state/state.decorator';

@Controller()
export class AppController {
  constructor(private readonly appService: AppService) {}

  @Post()
  async autofill(
    @Req() req: Request,
    @Body() body: AutofillPayload,
    @State() stateUrl: string,
  ): Promise<AutofillResponse> {
    const response = await this.appService.autofill(req, body, stateUrl);
    return response;
  }
}
