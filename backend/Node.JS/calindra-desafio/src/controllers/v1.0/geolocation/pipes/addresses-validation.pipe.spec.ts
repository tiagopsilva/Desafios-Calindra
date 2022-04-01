import { AddressesValidationPipe } from './addresses-validation.pipe';

describe(AddressesValidationPipe.constructor.name, () => {
  it('should be defined', () => {
    expect(new AddressesValidationPipe()).toBeDefined();
  });
});
