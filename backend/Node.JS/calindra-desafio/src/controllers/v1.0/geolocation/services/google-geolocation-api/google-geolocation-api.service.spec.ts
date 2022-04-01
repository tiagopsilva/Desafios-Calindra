import { Test, TestingModule } from '@nestjs/testing';
import { GoogleGeolocationApiService } from './google-geolocation-api.service';

describe(GoogleGeolocationApiService.constructor.name, () => {
  let service: GoogleGeolocationApiService;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      providers: [GoogleGeolocationApiService],
    }).compile();

    service = module.get<GoogleGeolocationApiService>(
      GoogleGeolocationApiService,
    );
  });

  it('should be defined', () => {
    expect(service).toBeDefined();
  });
});
