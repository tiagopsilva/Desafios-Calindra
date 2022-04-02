import { Addresses } from "../models/addresses.js";
import { GeolocationResponse } from "../models/geolocation/geolocation-response.js";
import { GeolocationResult } from "../models/geolocation/geolocation-result.js";
import { GeolocationReferencesInfo } from "../models/responses/geolocation-references-info.js";
import { MethodResult } from "../../../shared/results/method-result.js";
import { Result } from "../../../shared/results/result.js";
import { GoogleGeolocationApiService } from "./google-geolocation-api.service.js";
import { Location } from "../models/responses/location.js";
import { GeolocationInfo } from "../models/responses/geolocation-info.js";

export class DistanceCalculatorService {

    private readonly geolocationApiService: GoogleGeolocationApiService;

    constructor(geolocationApiService: GoogleGeolocationApiService) {
        this.geolocationApiService = geolocationApiService;
    }

    public async calculateDistancesFromAddressess(addresses: Addresses): Promise<MethodResult> {
        try {
            const { shorterQuantity, greatherQuantity } = this.calculateQuantities(addresses.addresses);
            const referencesInfo = await this.requestGeolocationReferencesInfo(addresses);
            referencesInfo.forEach(reference => {
                referencesInfo.filter(ref => ref != reference).forEach(ref => {
                    const geolocationInfo = this.mapGeolocationReferencesInfoToGeolocationInfo(ref);
                    geolocationInfo.distance = this.calculateEuclidianDistance(reference, ref);
                    reference.addAddressInfo(geolocationInfo);
                });
                this.orderByDistanceAndTakeShorthers(reference, shorterQuantity);
                this.orderByDescendingDistanceAndTakeGreathers(reference, greatherQuantity);
            });
            return Result.ok(referencesInfo);
        } catch (error) {
            return Result.fail('', 'Ocorreu uma falha no processamento da requisição');
        }
    }

    private async requestGeolocationReferencesInfo(addresses: Addresses): Promise<GeolocationReferencesInfo[]> {
        const promises = addresses.addresses.map(async address => {
            return await this.geolocationApiService.getGeolocationFrom(address);
        });
        const results: GeolocationResponse[] = await Promise.all(promises);
        return this.mapGeolocationResponsesToGeolocationReferencesInfo(results);
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
        reference.shorterDistances = reference.getAddressesInfo().sort((prev, actual) => {
            return prev.distance > actual.distance
                ? 1 : (prev.distance < actual.distance)
                    ? -1 : 0;
        }).slice(0, shorterQuantity);
    }

    private orderByDescendingDistanceAndTakeGreathers(reference: GeolocationReferencesInfo, greatherQuantity: number) {
        reference.greatherDistances = reference.getAddressesInfo().sort((prev, actual) => {
            return prev.distance > actual.distance
                ? -1 : (prev.distance < actual.distance)
                    ? 1 : 0;
        }).slice(0, greatherQuantity);
    }
}