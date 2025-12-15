import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlingService {

  constructor() { }

  handleError(error: HttpErrorResponse) {
    let errorMessage = 'error';

    if (error.status === 406) {
      errorMessage = error.error || 'Invalid login';
    } else if (error.status === 401) {
      errorMessage = 'Authentication failed';
    } else if (error.error instanceof ErrorEvent) {
      errorMessage = `Network error: ${error.error.message}`;
    } else if (error.status === 404) {
      errorMessage = 'User not found';
    } else {
      errorMessage = `Server error: ${error.status} - ${error.statusText}`;
    }

    console.error(errorMessage);
    return throwError(() => new Error(errorMessage));
  }
}
