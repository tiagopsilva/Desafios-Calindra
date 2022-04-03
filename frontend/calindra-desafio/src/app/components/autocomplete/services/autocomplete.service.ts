import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, map, Observable, of, tap } from 'rxjs';
import { AutoCompleteProductResult } from '../models/autocomplete-product-result';

import { AutoCompleteResult } from '../models/autocomplete-result';

@Injectable({
  providedIn: 'root'
})
export class AutocompleteService {

  private static URL = 'https://mystique-v2-americanas.juno.b2w.io/autocomplete?source=nanook&content=';

  constructor(private readonly http: HttpClient) { }

  public search(term: string): Observable<AutoCompleteProductResult[]> {
    if (!term?.trim()) {
      return of([]);
    }
    return this.http.get<AutoCompleteResult>(`${AutocompleteService.URL}${term}`)
      .pipe(
        map(result => result.products),
        catchError(error => {
          console.log(error);
          return [];
        })
      );
  }
}
