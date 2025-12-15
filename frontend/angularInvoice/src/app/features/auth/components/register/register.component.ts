import {Component, EventEmitter, Output} from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatDialogRef, MatDialogTitle, MatDialogContent, MatDialogActions } from '@angular/material/dialog';
import { AuthService } from '../../services/auth.service';
import { RegisterFormModel } from '../../models/register-form.model';
import { MatFormField, MatLabel, MatInput, MatError } from '@angular/material/input';
import { MatButton } from '@angular/material/button';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
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
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {

  registerForm: FormGroup;
  @Output() registrationSuccess = new EventEmitter<void>();
  errorMessage: string = '';
  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private dialogRef: MatDialogRef<RegisterComponent>
  ) {
    this.registerForm = this.fb.group({
      pseudo: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(1)]]
    });
  }

  onSubmit() {
    if (this.registerForm.valid) {
      const form: RegisterFormModel = this.registerForm.value;
      this.authService.register(form).subscribe(
        response => {
          this.onRegisterSuccess();
          this.dialogRef.close();
        },
        error => {
          this.errorMessage = error.message
          console.error('Error while registering:', error);
        }
      );
    }
  }

  close() {
    this.dialogRef.close();
  }
  onRegisterSuccess() {
    this.registrationSuccess.emit();
  }
}
