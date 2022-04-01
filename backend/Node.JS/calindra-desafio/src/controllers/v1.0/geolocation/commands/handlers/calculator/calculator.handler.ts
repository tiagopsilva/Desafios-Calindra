import { Injectable } from '@nestjs/common';
import { Observable } from 'rxjs';

import { CommandHandler } from '../command-handler';
import { DistanceCalculatorService } from '../../../services/distance-calculator/distance-calculator.service';
import { GeolocationReferencesInfo } from '../../../models/responses/geolocation-references-info';
import { Addresses } from '../../inputs/addresses';
import { MethodResult } from 'src/shared/results/method-result';


@Injectable()
export class CalculatorHandler implements CommandHandler<Addresses> {

    constructor(private distanceCalculatorService: DistanceCalculatorService) { }

    execute(command: Addresses): Observable<MethodResult> {
        return this.distanceCalculatorService.calculateDistancesFromAddressess(command);
    }
}
