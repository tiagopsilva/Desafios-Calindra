import { GeolocationResult } from './geolocation-result';

export class GeolocationResponse {
    public results: GeolocationResult[] = [];
    public status: string;
}