import {Component, inject} from '@angular/core';
import {AbstractControl, FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import { Button } from 'primeng/button';
import {FloatLabel} from 'primeng/floatlabel';
import { InputText } from 'primeng/inputtext';
import {FormErrorComponent} from '../../../../shared/form-error/form-error.component';
import {Router} from '@angular/router';
import {CustomerService} from '../../services/customer.service';
import {CustomerDetailsDtoModel} from '../../models/customer-details-dto.model';

@Component({
  selector: 'app-customer-create',
  imports: [
    FloatLabel,
    ReactiveFormsModule,
    InputText,
    Button,
    FormErrorComponent,
  ],
  templateUrl: './customer-create.component.html',
  styleUrl: './customer-create.component.scss'
})
export class CustomerCreateComponent {
  customerForm: FormGroup;

  private readonly _fb: FormBuilder = inject(FormBuilder);
  private readonly _router: Router = inject(Router);
  private readonly _customerService: CustomerService = inject(CustomerService);

  constructor() {
    this.customerForm = this._fb.group({
      name: [null, [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      address: [null, [Validators.minLength(3), Validators.maxLength(50)]],
      email: [null, [Validators.minLength(3), Validators.maxLength(50)]],
      phone: [null, [Validators.minLength(3), Validators.maxLength(50)]],
    });
  }

  submit(): void {
    this.customerForm.markAllAsTouched();

    if (this.customerForm.invalid) {
      console.log('Invalid form:', this.customerForm.value);
      return;
    }

    this._customerService.create(this.customerForm.value).subscribe({
      next: () => {
        console.log('Customer successfully created:');
        this._router.navigate(['/customer']);
      },
      error: err => {
        console.error('Error creating customer:', err);
      }
    });
  }
}
