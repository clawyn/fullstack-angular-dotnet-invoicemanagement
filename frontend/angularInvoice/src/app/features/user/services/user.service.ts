import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {AllUserResultModel} from '../models/all-user-result.model';
import {UpdateUserRequestModel} from '../models/update-user-request.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private readonly _http: HttpClient = inject(HttpClient);

  getAll(page: number, size: number) {
    return this._http.get<AllUserResultModel>(`http://localhost:5000user`, {
      params: {
        page: page.toString(),
        size: size.toString(),
        sort: 'login'
      }
    });
  }

  deleteUser(email: string) {
    return this._http.delete(`http://localhost:5000/user/${email}`);
  }

  updateUser(email: string,user: UpdateUserRequestModel) {
    return this._http.put<any>(`http://localhost:5000/user/${email}`, user);
  }

  addUser(user: UpdateUserRequestModel) {
    return this._http.post<any>(`http://localhost:5000/user`, user);
  }

}
