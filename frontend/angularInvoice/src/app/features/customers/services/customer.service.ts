import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {CustomerDetailsDtoModel} from '../models/customer-details-dto.model';
import {environment} from '../../../../environments/environment';
import {CustomerResponse} from '../models/customer-response';

@Injectable({
  providedIn: 'root',
})
export class CustomerService {

  private readonly _http: HttpClient = inject(HttpClient);

  private API_URL: string = "http://localhost:5000/api";

  constructor() {
  }


  create(customer: CustomerDetailsDtoModel) {
    return this._http.post<CustomerDetailsDtoModel>(`${this.API_URL}/customer`, customer);
  }

  delete(id: number) {
    return this._http.delete<void>(this.API_URL + `/customer/${id}`);
  }

  update(id: number, customerForm: CustomerDetailsDtoModel) {
    return this._http.put<void>(`${this.API_URL}/customer/${id}`, customerForm);
  }

  getCustomerById(id: number) {
    return this._http.get<CustomerDetailsDtoModel>(`${this.API_URL}/customer/${id}`);
  }

  getAll() {
    return this._http.get<CustomerDetailsDtoModel[]>(`${this.API_URL}/customer`);
  }

  getCustomerByIdString(id: string) {
    return this._http.get<CustomerDetailsDtoModel>(`${this.API_URL}/customer/${id}`);
  }


}
