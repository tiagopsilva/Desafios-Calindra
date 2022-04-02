import { Injectable } from '@nestjs/common';
import { forkJoin, map, Observable } from 'rxjs';

import { GoogleGeolocationApiService } from '../google-geolocation-api/google-geolocation-api.service';
import { Addresses } from '../../commands/inputs/addresses';
import { GeolocationReferencesInfo } from '../../models/responses/geolocation-references-info';
import { GeolocationResponse } from '../../models/geolocation/geolocation-response';
import { GeolocationResult } from '../../models/geolocation/geolocation-result';
import { Location } from '../../models/responses/location';
import { GeolocationInfo } from '../../models/responses/geolocation-info';
import { MethodResult } from 'src/shared/results/method-result';
import { Result } from 'src/shared/results/result';

@Injectable()
export class DistanceCalculatorService {

    constructor(private geolocationApiService: GoogleGeolocationApiService) { }

    public calculateDistancesFromAddressess(addresses: Addresses): Observable<MethodResult> {
        const { shorterQuantity, greatherQuantity } = this.calculateQuantities(addresses.addressList);
        const referencesInfo = this.requestGeolocationReferencesInfo(addresses);
        return referencesInfo.pipe(
            map((references: GeolocationReferencesInfo[]) => {
                try {
                    references.forEach(reference => {
                        references.filter(ref => ref != reference).forEach(ref => {
                            const geolocationInfo = this.mapGeolocationReferencesInfoToGeolocationInfo(ref);
                            geolocationInfo.distance = this.calculateEuclidianDistance(reference, ref);
                            reference.addresses.push(geolocationInfo);
                        });
                        this.orderByDistanceAndTakeShorthers(reference, shorterQuantity);
                        this.orderByDescendingDistanceAndTakeGreathers(reference, greatherQuantity);
                    });
                    return Result.ok(references);
                } catch (error) {
                    return Result.fail('', 'Ocorreu uma falha no processamento da requisição');
                }
            })
        );
    }

    private requestGeolocationReferencesInfo(addresses: Addresses): Observable<GeolocationReferencesInfo[]> {
        const tasks$ = addresses.addressList.map(address => this.geolocationApiService.getGeolocationFrom(address));
        const results = forkJoin(tasks$);
        return results.pipe(map((gelocationResponses: GeolocationResponse[]) => this.mapGeolocationResponsesToGeolocationReferencesInfo(gelocationResponses)));
    }

    private calculateQuantities(addresses: string[]) {
        const quantity = addresses.length - 1;
        const part = Math.trunc(quantity / 2);
        return {
            shorterQuantity: part,
            greatherQuantity: Math.trunc(part + (quantity % 2))
        }
    }

    private mapGeolocationResponsesToGeolocationReferencesInfo(gelocationResponses: GeolocationResponse[]): GeolocationReferencesInfo[] {
        let responsesOk: GeolocationResponse[] = gelocationResponses.filter(resp => resp.status == "OK");
        let results: GeolocationResult[] = [];
        responsesOk.map(r => results = results.concat(r.results));
        return results.map(result => {
            let ref = new GeolocationReferencesInfo();
            ref.placeId = result.place_id;
            ref.address = result.formatted_address;
            ref.location = new Location();
            ref.location.lat = result.geometry.location.lat;
            ref.location.lng = result.geometry.location.lng
            ref.shorterDistances = [];
            ref.greatherDistances = [];
            ref.addresses = [];
            return ref;
        });
    }

    private mapGeolocationReferencesInfoToGeolocationInfo(geolocationRefencesInfo: GeolocationReferencesInfo): GeolocationInfo {
        const geolocationInfo = new GeolocationInfo();
        geolocationInfo.placeId = geolocationRefencesInfo.placeId;
        geolocationInfo.address = geolocationRefencesInfo.address;
        geolocationInfo.location = geolocationRefencesInfo.location;
        return geolocationInfo;
    }

    private calculateEuclidianDistance(refA: GeolocationReferencesInfo, refB: GeolocationReferencesInfo): number {
        return Math.sqrt(
            Math.pow(refA.location.lat - refB.location.lat, 2) +
            Math.pow(refA.location.lng - refB.location.lng, 2)
        );
    }

    private orderByDistanceAndTakeShorthers(reference: GeolocationReferencesInfo, shorterQuantity: number) {
        reference.shorterDistances = reference.addresses.sort((prev, actual) => {
            return prev.distance > actual.distance
                ? 1 : (prev.distance < actual.distance)
                    ? -1 : 0;
        }).slice(0, shorterQuantity);
    }

    private orderByDescendingDistanceAndTakeGreathers(reference: GeolocationReferencesInfo, greatherQuantity: number) {
        reference.greatherDistances = reference.addresses.sort((prev, actual) => {
            return prev.distance > actual.distance
                ? -1 : (prev.distance < actual.distance)
                    ? 1 : 0;
        }).slice(0, greatherQuantity);
    }    
}
