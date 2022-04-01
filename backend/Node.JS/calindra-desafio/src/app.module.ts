import { Module } from '@nestjs/common';
import { APP_INTERCEPTOR } from '@nestjs/core';

import { GeolocationModule } from './controllers/v1.0/geolocation/geolocation.module';
import { TransformInterceptor } from './interceptors/transform.interceptor';

@Module({
  imports: [GeolocationModule],
  controllers: [],
  providers: [
    {
      provide: APP_INTERCEPTOR,
      useClass: TransformInterceptor,
    },
  ],
})
export class AppModule { }
