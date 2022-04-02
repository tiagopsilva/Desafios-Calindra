import axios, { AxiosStatic } from 'axios';
import { GeolocationResponse } from '../models/geolocation/geolocation-response.js';

const API_ACCESS_KEY_NAME = 'GOOGLE_GEOLOCATION_ACCESS_KEY';

export class GoogleGeolocationApiService {

    private readonly httpService: AxiosStatic;

    private API_ACCESS_KEY: string;
    private UrlBase = 'https://maps.googleapis.com';
    private UrlPath = '/maps/api/geocode/json';
  
    constructor() {
      this.httpService = axios;
      this.API_ACCESS_KEY = process.env[API_ACCESS_KEY_NAME] || '';
      if (!this.API_ACCESS_KEY)
        throw new Error(`Não foi encontrado a variável de ambiente ${API_ACCESS_KEY_NAME} com a chave da API do Google`);
    }
  
    async getGeolocationFrom(address: string) {
      const url = encodeURI(`${this.UrlBase}${this.UrlPath}?address=${address}&Key=${this.API_ACCESS_KEY}`);
      const response = await this.httpService.get<GeolocationResponse>(url, { params: { key: this.API_ACCESS_KEY } })
      return response.data;
    }
}