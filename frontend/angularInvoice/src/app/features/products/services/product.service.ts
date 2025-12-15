import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ProductDetailsDtoModel} from '../models/product-details.model';
import {ProductResponse} from '../models/product-response';


@Injectable({
  providedIn: 'root',
})
export class ProductService {

  private readonly _http: HttpClient = inject(HttpClient);

  private API_URL: string = "http://localhost:5000/api";

  constructor() {
  }


  createProduct(product: ProductDetailsDtoModel) {
    return this._http.post<ProductDetailsDtoModel>(`${this.API_URL}/product`, product);
  }

  delete(id: number) {
    return this._http.delete<void>(this.API_URL + `/product/${id}`);
  }

  update(id: number, productForm: ProductDetailsDtoModel) {
    return this._http.put<void>(`${this.API_URL}/product/${id}`, productForm);
  }

  getProductById(id: number) {
    return this._http.get<ProductDetailsDtoModel>(`${this.API_URL}/product/${id}`);
  }

  getAll() {
    return this._http.get<ProductDetailsDtoModel[]>(`${this.API_URL}/product`);
  }

  getPaginatedProduct(page: number, size: number) {
    return this._http.get<ProductResponse>(`${this.API_URL}/product?page=${page}&size=${size}`);
  }

  getProductByIdString(id: string) {
    return this._http.get<ProductDetailsDtoModel>(`${this.API_URL}/product/${id}`);
  }

}
