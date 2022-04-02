import { ApiProperty } from '@nestjs/swagger';
import { Command } from './command';

export class Addresses implements Command {

  @ApiProperty({
    description: 'List of addresses',
    minimum: 2
  })
  public addressList: string[];

}
