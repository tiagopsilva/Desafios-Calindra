import { Test, TestingModule } from '@nestjs/testing';

import { CalculatorHandler } from './calculator.handler';

describe(CalculatorHandler.constructor.name, () => {
  let service: CalculatorHandler;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      providers: [CalculatorHandler],
    }).compile();

    service = module.get<CalculatorHandler>(CalculatorHandler);
  });

  it('should be defined', () => {
    expect(service).toBeDefined();
  });
});
