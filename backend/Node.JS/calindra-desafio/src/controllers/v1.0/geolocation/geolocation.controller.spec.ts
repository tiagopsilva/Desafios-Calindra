import { Test, TestingModule } from '@nestjs/testing';
import { GeolocationController } from './geolocation.controller';

describe(GeolocationController.constructor.name, () => {
  let controller: GeolocationController;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      controllers: [GeolocationController],
    }).compile();

    controller = module.get<GeolocationController>(GeolocationController);
  });

  it('should be defined', () => {
    expect(controller).toBeDefined();
  });
});
