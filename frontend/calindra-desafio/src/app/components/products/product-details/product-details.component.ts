import { Component } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { Observable } from 'rxjs';

import { ProductService } from '../services/product/product.service';
import { Product } from '../models/product';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent {

  public product$ = new Observable<Product>();

  constructor(
    router: Router,
    private route: ActivatedRoute,
    private readonly productService: ProductService
  ) {
    router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.product$ = this.productService.getProduct(this.route.snapshot.params["id"]);
      }
    });
  }

  public getImageUrl(product: Product): string {
    if (product?.images?.length && product.images.length > 0) {
      const image = product.images[0];
      return image.extraLarge || image.large || image.medium || image.big || '';
    }
    return '';
  }
}
