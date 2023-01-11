import { NestFactory } from '@nestjs/core';
import { AppModule } from './app.module';

async function bootstrap() {
  const app = await NestFactory.create(AppModule);

  // setting the marketplace port
  let listenPort: number = 3000;
  const smpPort = process.env['SMP_PORT'];
  if (smpPort) {
    const port = parseInt(smpPort);
    if (!isNaN(port)) {
      listenPort = port;
    }
  }

  await app.listen(listenPort);
}
bootstrap();
