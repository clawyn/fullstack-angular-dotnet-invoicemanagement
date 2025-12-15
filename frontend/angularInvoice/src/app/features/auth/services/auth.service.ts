import { inject, Injectable, signal, WritableSignal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserTokenDto } from '../models/user-token-dto';
import { RegisterFormModel } from '../models/register-form.model';
import { LoginFormModel } from '../models/login-form.model';
import {catchError, of, tap} from 'rxjs';
import { ErrorHandlingService } from './error-handling.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private readonly _http: HttpClient = inject(HttpClient);
  private errorHandlingService: ErrorHandlingService = inject(ErrorHandlingService);

  currentUser: WritableSignal<UserTokenDto | undefined>;

  private API_URL: string = "http://localhost:5000/api/user";

  constructor() {
    let jsonUser = sessionStorage.getItem('currentUser');
    this.currentUser = signal(jsonUser ? JSON.parse(jsonUser) : undefined);
  }

  register(form: RegisterFormModel) {
    console.log('Register form submitted:', form);
    return this._http.post<void>(`${this.API_URL}/register`, form).pipe(
      catchError((error) => {

        this.errorHandlingService.handleError(error);

        return of(null);
      })
    );
  }


  login(form: LoginFormModel) {
    return this._http.post<UserTokenDto>(`${this.API_URL}/login`, form).pipe(
      tap((result: any) => {
        const userToken: UserTokenDto = {
          accessToken: result.token,
          user: {
            id: result.user.id,
            login: result.user.pseudo,
            email: ''
          }
        };


        console.log('UserToken DTO généré :', userToken);

        this.currentUser.set(userToken);
        sessionStorage.setItem('currentUser', JSON.stringify(userToken));
      })
    );
  }

  logout() {
    sessionStorage.removeItem('currentUser');
    this.currentUser.set(undefined);
  }
}
