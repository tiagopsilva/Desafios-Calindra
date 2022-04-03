import { Component, OnInit } from '@angular/core';
import { forkJoin, map, Observable } from 'rxjs';

import { ProductService } from '../services/product/product.service';
import { AutocompleteService } from '../../autocomplete/services/autocomplete.service';
import { GridService } from '../services/grid/grid.service';
import { Product } from '../models/product';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';

@Component({
  selector: 'app-product-grid',
  templateUrl: './product-grid.component.html',
  styleUrls: ['./product-grid.component.css']
})
export class ProductGridComponent implements OnInit {

  public products: Product[] = [];

  public gridColumns: number = 0;

  constructor(
    router: Router,
    private route: ActivatedRoute,
    private readonly autocompleteService: AutocompleteService,
    private readonly productService: ProductService,
    private readonly gridService: GridService
  ) {
    router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.autocompleteService.search(this.route.snapshot.params["text"])
          .pipe(
            map(results => {
              const observables$ = results.map((result) => this.productService.getProduct(result.id));
              return forkJoin(observables$)
            })
          )
          .subscribe(task => task.subscribe(products => this.products = products))
      }
    })
  }

  ngOnInit(): void {
    this.gridColumns = this.gridService.getColumnsSize(window.innerWidth);
  }

  public handleSize(event: any) {
    this.gridColumns = this.gridService.getColumnsSize(event.target.innerWidth);
  }
}
