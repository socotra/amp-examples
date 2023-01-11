import { createParamDecorator } from '@nestjs/common';

export const State = createParamDecorator(() => {
  return process.env['SMP_STATE_API'];
});
