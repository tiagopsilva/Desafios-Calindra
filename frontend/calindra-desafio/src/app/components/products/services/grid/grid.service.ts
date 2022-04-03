import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class GridService {

  constructor() { }

  public getColumnsSize(width: number): number {
    return (width <= 624) ? 1
      : (width <= 800) ? 2
        : (width <= 1200) ? 3
          : (width <= 1500) ? 4
            : (width <= 1800) ? 6
              : 8;
  }
}
