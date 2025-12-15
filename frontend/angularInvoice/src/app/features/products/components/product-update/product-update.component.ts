import {Component, inject, Input} from '@angular/core';
import {Button} from "primeng/button";
import {FloatLabel} from "primeng/floatlabel";
import {FormErrorComponent} from "../../../../shared/form-error/form-error.component";
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {InputText} from "primeng/inputtext";
import {InputNumber} from 'primeng/inputnumber';
import {ActivatedRoute, Router} from '@angular/router';
import {ProductService} from '../../services/product.service';
import {ProductDetailsDtoModel} from '../../models/product-details.model';

@Component({
  selector: 'app-product-update',
  imports: [
    Button,
    FloatLabel,
    FormErrorComponent,
    FormsModule,
    InputText,
    ReactiveFormsModule,
    InputNumber
  ],
  templateUrl: './product-update.component.html',
  styleUrl: './product-update.component.scss'
})
export class ProductUpdateComponent {
  productId!: number;
  productIdString!: string;
  errorMsg: string = '';

  @Input({required: true})
  product!: ProductDetailsDtoModel;
  productForm: FormGroup;

  constructor(
    private readonly _fb: FormBuilder = inject(FormBuilder),
    private readonly _ar: ActivatedRoute,
    private readonly _productService: ProductService = inject(ProductService),
    private readonly _router: Router = inject(Router),
    private readonly _route: ActivatedRoute,
  ) {
    this.productForm = this._fb.group({
      id: [null, Validators.required],
      productName: [null, [Validators.minLength(3), Validators.maxLength(50)]],
      description: [null, [Validators.maxLength(255)]],
      category: [null, [Validators.minLength(3), Validators.maxLength(50)]],
      specification: [null, [Validators.minLength(3), Validators.maxLength(50)]],
      price: [null, [Validators.required, Validators.min(1)]],
    });
  }

  ngOnInit(): void {
    const id = this._route.snapshot.paramMap.get('id');

    if (!id) {
      console.error("ID manquant dans l'URL");
      return;
    }

    this.productIdString = id;

    this._productService.getProductByIdString(id).subscribe({
      next: (product) => {
        this.productForm.patchValue(product);
      },
      error: (err) => console.error('Error fetching product data:', err)
    });
  }

  updateProduct(): void {
    if (this.productForm.invalid) {
      console.log('Invalid form', this.productForm.value);
      return;
    }

    const productId = this.productForm.value.id;


    const productData = {
      ...this.productForm.value,
    };

    console.log('Data sent to the backend:', productData);

    this._productService.update(productId, productData).subscribe({
      next: (updatedProduct) => {
        console.log('sucecessfully:', updatedProduct);
        this._router.navigate(['/products']);
      },
      error: (err) => {
        console.error('Error updating product:', err);
      }
    });
  }
}
