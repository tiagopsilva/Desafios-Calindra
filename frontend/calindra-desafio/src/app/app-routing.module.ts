import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ProductDetailsComponent } from './components/products/product-details/product-details.component';
import { ProductGridComponent } from './components/products/product-grid/product-grid.component';

const routes: Routes = [
  { path: 'product/:id/:name', component: ProductDetailsComponent },
  { path: 'search/:text', component: ProductGridComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
