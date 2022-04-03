import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable } from 'rxjs';

import { Product } from '../../models/product';
import { ProductResponse } from '../../models/product-response';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private static URL: string = 'https://restql-server-api-v2-americanas.b2w.io/run-query/catalogo/product-buybox/12?id=';

  constructor(private readonly http: HttpClient) { }

  public getProduct(id: string): Observable<Product> {
    return this.http.get<ProductResponse>(`${ProductService.URL}${id}`)
      .pipe(
        map(response => response.product.result),
        catchError(response => {
          console.log(response);
          return [];
        })
      );
  }
}
