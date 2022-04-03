import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ProductService } from './product.service';
import * as fs from 'fs';

const API_URL = 'https://mystique-v2-americanas.juno.b2w.io/autocomplete?source=nanook&content=a';

describe(ProductService.constructor.name, () => {
  let service: ProductService;
  let httpController: HttpTestingController;

  const fileContent = fs.readFileSync('./product-56664056-response-for-test.json', 'utf-8');
  const jsonResult = JSON.parse(fileContent);

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers:[ProductService]
    });
    service = TestBed.inject(ProductService);
    httpController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => httpController.verify());

  it('Precisa estar criado', () => {
    expect(service).toBeTruthy();
  });

  it(`#${ProductService.prototype.getProduct.name} precisa retornar as informacoes do produto`, (done) => {
    console.group(jsonResult.product.result)
    service.getProduct(jsonResult.product.result.id).subscribe(product => {
      expect(product).not.toBeNull()
      expect(JSON.stringify(product)).toEqual(JSON.stringify(jsonResult.product));
    });
    httpController.expectOne(API_URL).flush(jsonResult.product);
  });
});
