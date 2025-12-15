import {Component, inject, Input} from '@angular/core';
import {Button} from "primeng/button";
import {FloatLabel} from "primeng/floatlabel";
import {FormErrorComponent} from "../../../../shared/form-error/form-error.component";
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {InputText} from "primeng/inputtext";
import {ActivatedRoute, Router} from '@angular/router';
import {FieldsetModule} from 'primeng/fieldset';
import {DropdownModule} from 'primeng/dropdown';
import {CustomerDetailsDtoModel} from '../../models/customer-details-dto.model';
import {CustomerService} from '../../services/customer.service';



@Component({
  selector: 'app-customer-update',
    imports: [
      Button,
      ReactiveFormsModule,
      InputText,
      FloatLabel,
      FieldsetModule,
      DropdownModule,
      FormsModule,
      Button,
      FormErrorComponent,
    ],
  templateUrl: './customer-update.component.html',
  styleUrl: './customer-update.component.scss'
})
export class CustomerUpdateComponent {
  customerId!: number;
  customerIdString!: string;
  errorMsg: string = '';

  @Input({required: true})
  customer!: CustomerDetailsDtoModel;
  customerForm: FormGroup;

  constructor(
    private readonly _fb: FormBuilder = inject(FormBuilder),
    private readonly _ar: ActivatedRoute,
    private readonly _customerService: CustomerService = inject(CustomerService),
    private readonly _router: Router = inject(Router),
    private readonly _route: ActivatedRoute,
  ) {
    this.customerForm = this._fb.group({
      id: [null, Validators.required],
      name: [null, [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      address: [null, [Validators.minLength(3), Validators.maxLength(50)]],
      email: [null, [Validators.minLength(3), Validators.maxLength(50)]],
      phone: [null, [Validators.minLength(3), Validators.maxLength(50)]],
    });
  }

  ngOnInit(): void {
    const id = this._route.snapshot.paramMap.get('id');

    if (!id) {
      console.error("ID manquant dans l'URL");
      return;
    }

    this.customerIdString = id;

    this._customerService.getCustomerByIdString(id).subscribe({
      next: (customer) => {
        this.customerForm.patchValue(customer);
      },
      error: (err) => console.error('Error fetching customer data:', err)
    });
  }


  updateCustomer(): void {
    if (this.customerForm.invalid) {
      console.log('Invalid form', this.customerForm.value);
      return;
    }

    const customerId = this.customerForm.value.id;

    const customerData = {
      ...this.customerForm.value,
    };

    console.log('Data sent to the backend:', customerData);

    this._customerService.update(customerId, customerData).subscribe({
      next: (updatedCustomer) => {
        console.log('sucecessfully:', updatedCustomer);
        this._router.navigate(['/customer']);
      },
      error: (err) => {
        console.error('Error updating customer:', err);
      }
    });
  }
}
