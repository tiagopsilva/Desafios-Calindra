import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { TextToUrlPipe } from 'src/shared/pipes/text-to-url.pipe';
import { Product } from '../models/product';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent {

  @Input()
  public product: Product | null = null;

  constructor() { }

  public getImageUrl(product: Product): string {
    if (product?.images?.length && product.images.length > 0) {
      const image = product.images[0];
      return image.extraLarge || image.large || image.medium || image.big || '';
    }
    return '';
  }

  public productUrl(product: Product | null): string {
    if (!product)
      return '';
    var productName = new TextToUrlPipe().transform(product.name);
    return `${product.id}/${productName}`;
  }
}
