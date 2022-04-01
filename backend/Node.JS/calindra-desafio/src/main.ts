import { AppModule } from './app.module';
import { VersioningType } from '@nestjs/common';
import { NestFactory } from '@nestjs/core';
import { DocumentBuilder, SwaggerModule } from '@nestjs/swagger';

import { TransformInterceptor } from './interceptors/transform.interceptor';

async function bootstrap() {
  const app = (await NestFactory.create(AppModule))
    .setGlobalPrefix('api')
    .useGlobalInterceptors(new TransformInterceptor())
    .enableVersioning({ type: VersioningType.URI });

  const config = new DocumentBuilder()
    .setTitle('Desafio Calindra')
    .setDescription('Desafio proposto pela Calindra com a intenção de entender como o dev pensa na hora de abordar os problemas.')
    .setVersion('1.0')
    .build();

  const document = SwaggerModule.createDocument(app, config);
  SwaggerModule.setup('api', app, document);

  await app.listen(3000);
}
bootstrap();
