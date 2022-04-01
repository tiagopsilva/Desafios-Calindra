import { Body, Controller, HttpCode, Post, Res, UsePipes } from '@nestjs/common';
import { ApiResponse } from '@nestjs/swagger';
import { Response } from 'express';
import { map, Observable } from 'rxjs';
import { ControllerBase } from '../../base/controller-base';
import { CalculatorHandler } from './commands/handlers/calculator/calculator.handler';

import { Addresses } from './commands/inputs/addresses';
import { AddressesValidationPipe } from './pipes/addresses-validation.pipe';

@Controller({
    version: '1',
    path: 'geolocation'
})
export class GeolocationController extends ControllerBase {

    constructor(private handler: CalculatorHandler) {
        super()
    }

    @Post()
    @HttpCode(200)
    @UsePipes(new AddressesValidationPipe())
    @ApiResponse({ status: 200, description: 'The addresses has been successfully calculated' })
    public post(@Body() addresses: Addresses, @Res() response: Response): Observable<void> {
        return this.handler.execute(addresses)
            .pipe(map(result => this.process(result, response)));
    }
}
