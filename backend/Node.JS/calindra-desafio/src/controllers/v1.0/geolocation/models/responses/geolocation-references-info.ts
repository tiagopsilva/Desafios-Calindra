import { Exclude } from 'class-transformer';

import { GeolocationInfo } from './geolocation-info';
import { Location } from './location';

export class GeolocationReferencesInfo {
  public placeId: string;
  public address: string;
  public location: Location;
  public greatherDistances: GeolocationInfo[];
  public shorterDistances: GeolocationInfo[];

  @Exclude()
  public addresses: GeolocationInfo[];
}
