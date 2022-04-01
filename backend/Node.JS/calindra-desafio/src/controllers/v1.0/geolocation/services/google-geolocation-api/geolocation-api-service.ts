import { Observable } from 'rxjs';

import { GeolocationResponse } from '../../models/geolocation/geolocation-response';

export interface GeolocationApiService {
  getGeolocationFrom(address: string): Observable<GeolocationResponse>;
}
