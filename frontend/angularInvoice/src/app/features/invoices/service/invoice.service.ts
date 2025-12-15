import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {InvoiceDetailsDtoModel} from '../models/invoice-details.model';
import {ProductDetailsDtoModel} from '../../products/models/product-details.model';

@Injectable({
  providedIn: 'root',
})
export class InvoiceService {
  private readonly _http: HttpClient = inject(HttpClient);

  private API_URL: string = "http://localhost:5000/api";

  constructor() {
  }
  createInvoice(invoice: InvoiceDetailsDtoModel) {
    return this._http.post<InvoiceDetailsDtoModel>(`${this.API_URL}/invoice`, invoice);
  }

  delete(id: number) {
    return this._http.delete<void>(this.API_URL + `/invoice/${id}`);
  }

  getAll() {
    return this._http.get<InvoiceDetailsDtoModel[]>(`${this.API_URL}/invoice`);
  }

  getInvoiceByIdString(id: string) {
    return this._http.get<InvoiceDetailsDtoModel>(`${this.API_URL}/invoice/${id}`);
  }

  getInvoicesByCustomerId(customerId: string) {
    console.log('Requête envoyée au backend avec Customer ID:', customerId);
    return this._http.get<InvoiceDetailsDtoModel[]>(`${this.API_URL}/Invoice/customer/${customerId}/invoices`);
  }
}
