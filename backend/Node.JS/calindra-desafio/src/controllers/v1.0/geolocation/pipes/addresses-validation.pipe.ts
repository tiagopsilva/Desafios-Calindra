import { ArgumentMetadata, BadRequestException, HttpStatus, Injectable, PipeTransform } from '@nestjs/common';
import { Addresses } from '../commands/inputs/addresses';

@Injectable()
export class AddressesValidationPipe implements PipeTransform {
  transform(addresses: Addresses, metadata: ArgumentMetadata) {
    if (!addresses?.addresses || addresses.addresses.length < 2)
      throw new BadRequestException({
        statusCode: HttpStatus.BAD_REQUEST,
        message: 'The list of addresses need 2 or more address',
        error: 'Bad Request',
      });
    return addresses;
  }
}