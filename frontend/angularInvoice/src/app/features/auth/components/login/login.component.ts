import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatDialogRef, MatDialogTitle, MatDialogContent, MatDialogActions } from '@angular/material/dialog';
import { AuthService } from '../../services/auth.service';
import { LoginFormModel } from '../../models/login-form.model';
import { MatFormField, MatLabel, MatInput, MatError } from '@angular/material/input';
import { MatButton } from '@angular/material/button';
import { NgIf } from '@angular/common';
import {Router} from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  imports: [
    MatDialogContent,
    MatDialogActions,
    MatFormField,
    MatLabel,
    MatInput,
    MatError,
    ReactiveFormsModule,
    MatButton,
    NgIf,
    MatDialogTitle,
  ],
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  loginForm: FormGroup;
  errorMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private dialogRef: MatDialogRef<LoginComponent>,
    private router: Router,
  ) {
    this.loginForm = this.fb.group({
      login: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }

  onSubmit() {

    const loginFormValues = this.loginForm.value;
    console.log("Login Form Values:", loginFormValues);

    if (this.loginForm.valid) {
      const form: LoginFormModel = this.loginForm.value;
      this.authService.login(form).subscribe(
        (response) => {
          this.dialogRef.close();
          this.router.navigate(['/home']);
        },
        (error) => {
          this.errorMessage = error.message || 'Une erreur est survenue lors de la connexion.';
          console.error('Erreur lors de la connexion:', error);
        }
      );
    }
  }

  close() {
    this.dialogRef.close();
  }
}
