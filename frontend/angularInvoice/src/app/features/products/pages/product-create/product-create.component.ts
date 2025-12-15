import {Component, inject} from '@angular/core';
import {Button} from 'primeng/button';
import {FloatLabel} from 'primeng/floatlabel';
import {FormErrorComponent} from '../../../../shared/form-error/form-error.component';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import {InputText} from 'primeng/inputtext';
import {InputNumber} from 'primeng/inputnumber';
import {Router} from '@angular/router';
import {ProductService} from '../../services/product.service';

@Component({
  selector: 'app-product-create',
  imports: [
    Button,
    FloatLabel,
    FormErrorComponent,
    FormsModule,
    InputText,
    ReactiveFormsModule,
    InputNumber
  ],
  templateUrl: './product-create.component.html',
  styleUrl: './product-create.component.scss'
})
export class ProductCreateComponent {
  productForm: FormGroup;

  private readonly _fb: FormBuilder = inject(FormBuilder);
  private readonly _router: Router = inject(Router);
  private readonly _productService: ProductService = inject(ProductService);

  constructor() {
    this.productForm = this._fb.group({
      productName: [null, [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      description: [null, [Validators.maxLength(255)]],
      category: [null,  [Validators.minLength(3), Validators.maxLength(50)]],
      specification: [null,  [Validators.minLength(3), Validators.maxLength(50)]],
      price: [null, [Validators.required, Validators.min(1)]],
    });
  }

  submit(): void {
    this.productForm.markAllAsTouched();

    const productFormValues = this.productForm.value;
    console.log("Product Form Values:", productFormValues);

    if (this.productForm.invalid) {
      console.log('Invalid form:', this.productForm.value);
      return;
    }

    this._productService.createProduct(this.productForm.value).subscribe({
      next: () => {
        console.log('Product successfully created:');
        this._router.navigate(['/products']);
      },
      error: err => {
        console.error('Error creating product:', err);
      }
    });
  }
}
