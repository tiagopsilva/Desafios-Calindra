import { Injectable } from '@nestjs/common';
import { HttpService } from '@nestjs/axios';
import { map, Observable } from 'rxjs';

import { GeolocationApiService } from './geolocation-api-service';
import { GeolocationResponse } from '../../models/geolocation/geolocation-response';

const API_ACCESS_KEY_NAME = 'GOOGLE_GEOLOCATION_ACCESS_KEY';

@Injectable()
export class GoogleGeolocationApiService implements GeolocationApiService {

  private API_ACCESS_KEY: string;
  private UrlBase = 'https://maps.googleapis.com';
  private UrlPath = '/maps/api/geocode/json';

  constructor(private httpService: HttpService) {
    this.API_ACCESS_KEY = process.env[API_ACCESS_KEY_NAME] || '';
    if (!this.API_ACCESS_KEY)
      throw new Error(`Não foi encontrado a variável de ambiente ${API_ACCESS_KEY_NAME} com a chave da API do Google`);
  }

  getGeolocationFrom(address: string): Observable<GeolocationResponse> {
    const url = encodeURI(`${this.UrlBase}${this.UrlPath}?address=${address}&Key=${this.API_ACCESS_KEY}`);
    return this.httpService.get<GeolocationResponse>(url, { params: { key: this.API_ACCESS_KEY } })
      .pipe(map(response => response.data));
  }
}
