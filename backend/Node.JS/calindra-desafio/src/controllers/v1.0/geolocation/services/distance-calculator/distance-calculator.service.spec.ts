import { Test, TestingModule } from '@nestjs/testing';
import { DistanceCalculatorService } from './distance-calculator.service';

describe(DistanceCalculatorService.constructor.name, () => {
  let service: DistanceCalculatorService;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      providers: [DistanceCalculatorService],
    }).compile();

    service = module.get<DistanceCalculatorService>(DistanceCalculatorService);
  });

  it('should be defined', () => {
    expect(service).toBeDefined();
  });
});
