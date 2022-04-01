import { TransformInterceptor } from './transform.interceptor';

describe(TransformInterceptor.constructor.name, () => {
  it('should be defined', () => {
    expect(new TransformInterceptor()).toBeDefined();
  });
});
