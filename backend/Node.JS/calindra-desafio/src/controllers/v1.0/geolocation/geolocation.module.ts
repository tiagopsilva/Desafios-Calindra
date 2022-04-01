import { Module } from '@nestjs/common';
import { HttpModule } from '@nestjs/axios';

import { GeolocationController } from './geolocation.controller';
import { GoogleGeolocationApiService } from './services/google-geolocation-api/google-geolocation-api.service';
import { DistanceCalculatorService } from './services/distance-calculator/distance-calculator.service';
import { CalculatorHandler } from './commands/handlers/calculator/calculator.handler';

@Module({
  imports: [HttpModule],
  controllers: [GeolocationController],
  providers: [GoogleGeolocationApiService, DistanceCalculatorService, CalculatorHandler],
})
export class GeolocationModule {}
