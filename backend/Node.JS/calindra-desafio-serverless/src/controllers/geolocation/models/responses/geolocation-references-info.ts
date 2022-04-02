import { GeolocationInfo } from './geolocation-info';
import { Location } from './location';

export class GeolocationReferencesInfo {
  private addressesInfo: GeolocationInfo[] = [];

  public placeId: string;
  public address: string;
  public location: Location;
  public greatherDistances: GeolocationInfo[];
  public shorterDistances: GeolocationInfo[];

  public addAddressInfo(addressInfo: GeolocationInfo) {
    this.addressesInfo.push(addressInfo);
  }

  public getAddressesInfo(): GeolocationInfo[] {
    return this.addressesInfo;
  }
}
