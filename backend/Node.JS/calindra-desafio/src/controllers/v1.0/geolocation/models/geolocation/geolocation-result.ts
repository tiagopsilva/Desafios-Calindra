import { GeolocationGeometry } from './geolocation-geometry';

export class GeolocationResult {
    public place_id: string;
    public formatted_address: string;
    public geometry: GeolocationGeometry;
}