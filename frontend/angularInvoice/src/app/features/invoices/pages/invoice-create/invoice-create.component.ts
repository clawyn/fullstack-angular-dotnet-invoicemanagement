import {Component, inject, ViewChild} from '@angular/core';
import {Button} from 'primeng/button';
import {FloatLabel} from 'primeng/floatlabel';
import {FormErrorComponent} from '../../../../shared/form-error/form-error.component';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import {InputText} from 'primeng/inputtext';
import {Router} from '@angular/router';
import {InvoiceService} from '../../service/invoice.service';
import { FormArray } from '@angular/forms';
import {CustomerDetailsDtoModel} from '../../../customers/models/customer-details-dto.model';
import {ProductDetailsDtoModel} from '../../../products/models/product-details.model';
import {NgForOf, NgIf, NgStyle} from '@angular/common';
import {CustomerService} from '../../../customers/services/customer.service';
import {ProductService} from '../../../products/services/product.service';
import {InputNumber} from 'primeng/inputnumber'

@Component({
  selector: 'app-invoice-create',
  templateUrl: './invoice-create.component.html',
  imports: [
    Button,
    FloatLabel,
    FormErrorComponent,
    FormsModule,
    InputText,
    ReactiveFormsModule,
    NgForOf,
    InputNumber,
    NgStyle,
    NgIf,

  ],
  styleUrls: ['./invoice-create.component.scss']
})
export class InvoiceCreateComponent {
  @ViewChild('quantityInput') quantityInput!:  { value: number };
  invoiceForm: FormGroup;
  customersList: CustomerDetailsDtoModel[] = [];
  productsList: ProductDetailsDtoModel[] = [];
  isLoadingClients: boolean = true;
  selectedProductId: string | null = null;
  isQuantityValid = false;

  getQuantityValue(): number {
    return this.quantityInput.value ?? 1;
  }

  private readonly _fb: FormBuilder = inject(FormBuilder);
  private readonly _router: Router = inject(Router);
  private readonly _invoiceService: InvoiceService = inject(InvoiceService);
  private readonly _productService: ProductService = inject(ProductService);
  private readonly _customerService: CustomerService = inject(CustomerService);

  constructor() {
    this.invoiceForm = this._fb.group({
      name: [null, [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      description: [null, [Validators.maxLength(255)]],
      workForce: [0, [Validators.required, Validators.min(0)]],
      customerId: [null, [Validators.required]],
      products: this._fb.array([])
    });
  }

  get products(): FormArray {
    return this.invoiceForm.get('products') as FormArray;
  }

  addProduct(productId: string, quantity: number): void {
    this.products.push(this._fb.group({
      productId: [productId, Validators.required],
      quantity: [quantity, [Validators.required, Validators.min(1)]],
    }));
  }
  onProductSelect(event: Event): void {
    const target = event.target as HTMLSelectElement;
    const productId = target.value;

    const quantity = this.quantityInput?.value ?? 0;
    if (!productId || quantity <= 0) {
      alert('Veuillez entrer une quantité valide avant de sélectionner un produit.');
      return;
    }
    if (productId) {
      const quantityProduct = this.getQuantityValue();
      this.selectedProductId = productId;
      console.log('Quantité récupérée :', quantityProduct);

      this.addProduct(productId, quantityProduct);

    }
  }

  validateQuantity(): void {
    const quantity = this.getQuantityValue();
    if (quantity > 0) {
      this.isQuantityValid = true;
      console.log('Quantité validée :', quantity);
    } else {
      alert('Veuillez entrer une quantité valide');
    }
  }

  ngOnInit(): void {
    this._productService.getAll().subscribe({
      next: (products) => {
        console.log('Produits récupérés:', products);
        this.productsList = products;
      },
      error: (err) => console.error('Erreur produits:', err)
    });

    this._customerService.getAll().subscribe({
      next: (customers) => {
        console.log('Clients récupérés:', customers);
        this.customersList = customers;
        this.isLoadingClients = false;
      },
      error: (err) => {
        console.error('Erreur clients:', err);
        this.isLoadingClients = false;
      }

    });
  }

  submit(): void {
    this.invoiceForm.markAllAsTouched();

    if (this.invoiceForm.invalid) {
      console.log('Invalid form:', this.invoiceForm.value);
      const quantity = this.getQuantityValue();
      console.log('Quantité récupérée :', quantity);
      return;
    }

    const quantity = this.getQuantityValue();
    const productId = this.selectedProductId;

    console.log('Quantité sélectionnée au moment du submit :', quantity);
    console.log('Produit sélectionné :', productId);

    console.log('Données de la facture à envoyer:', this.invoiceForm.value);

    if (productId && quantity > 1) {
      const alreadyAdded = this.products.controls.some(ctrl => ctrl.value.productId === productId);
      if (!alreadyAdded) {
        this.addProduct(productId, quantity);
      }
    }

    const invoiceToSend = this.invoiceForm.value;
    console.log('Données de la facture à envoyer:', invoiceToSend);
    this._invoiceService.createInvoice(invoiceToSend).subscribe({
      next: () => {
        console.log('Invoice successfully created');
        this._router.navigate(['/customer']);
      },
      error: err => {
        console.error('Error creating invoice:', err);
      }
    });
  }
}
