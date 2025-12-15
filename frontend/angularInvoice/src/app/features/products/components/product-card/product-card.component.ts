import {Component, computed, EventEmitter, inject, Input, Output} from '@angular/core';
import {Card} from 'primeng/card';
import {ButtonDirective} from 'primeng/button';
import {Router} from '@angular/router';
import {ProductService} from '../../services/product.service';
import {ProductDetailsDtoModel} from '../../models/product-details.model';
import {ProductResponse} from '../../models/product-response';
import {NgIf} from '@angular/common';
import {AuthService} from '../../../auth/services/auth.service';

@Component({
  selector: 'app-product-card',
  imports: [
    Card,
    ButtonDirective,
    NgIf,
  ],
  templateUrl: './product-card.component.html',
  styleUrl: './product-card.component.scss'
})
export class ProductCardComponent {
  @Input({required: true})
  product!: ProductDetailsDtoModel;
  products!: ProductResponse;

  constructor(
    private readonly _authService: AuthService = inject(AuthService),
    private readonly _productService: ProductService = inject(ProductService),
    private readonly _router: Router = inject(Router)
  ) {
  }

  @Output() productDeleted = new EventEmitter<void>();


  deleteProduct(productId: number): void {
    if (confirm('Are you sure you want to delete this product ?')) {
      this._productService.delete(productId).subscribe({
        next: () => {
          console.log('Product deleted successfully');


          this.productDeleted.emit();
          this._router.navigate(['/product']);
        },
        error: (err) => {
          console.error('Error deleting product', err);
        }
      });
    }
  }

  update(productId: number): void {
    console.log('Updating product with ID:', productId);
    this._router.navigate([`/product/update/${productId}`]);
  }

  ngOnInit(): void {
    console.log("Produit chargé :", this.product);
  }
  userLogin = computed(() => this._authService.currentUser()?.user.login ?? '');
}
