import {Component, computed, inject, Input, Signal, WritableSignal} from '@angular/core';

import {Button} from 'primeng/button';
import {RouterLink} from '@angular/router';
import { TableModule } from 'primeng/table';

import {Paginator, PaginatorState} from 'primeng/paginator';
import {AuthService} from '../../../auth/services/auth.service';
import {UserTokenDto} from '../../../auth/models/user-token-dto';
import {ProductService} from '../../services/product.service';
import {ProductDetailsDtoModel} from '../../models/product-details.model';
import {ProductCardComponent} from '../../components/product-card/product-card.component';

@Component({
  selector: 'app-product-index',
  imports: [
    Button,
    RouterLink,
    TableModule,
    Paginator,
    ProductCardComponent
  ],
  templateUrl: './product-index.component.html',
  styleUrl: './product-index.component.scss'
})
export class ProductIndexComponent {
  private readonly _productService: ProductService = inject(ProductService);
  private readonly _authService: AuthService = inject(AuthService);

  @Input()
  products: ProductDetailsDtoModel[] = [] as ProductDetailsDtoModel[];
  allProducts: ProductDetailsDtoModel[] = [];

  currentPage: number = 1;
  totalPages: number = 1;
  pageSize: number = 6;

  currentUser: WritableSignal<UserTokenDto | undefined>;
  isConnected: Signal<boolean>;

  constructor() {
    this.currentUser = this._authService.currentUser
    this.isConnected = computed(() => {
      return !!this.currentUser();
    });
    this.loadProducts();
  }

  loadProducts() {
    this._productService.getAll().subscribe({
      next: (data: ProductDetailsDtoModel[]) => {
        this.allProducts = data;
        this.totalPages = Math.ceil(this.allProducts.length / this.pageSize);
        this.updatePagedProducts();
      },
      error: (err) => console.log(err),
    });
  }

  updatePagedProducts() {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    this.products = this.allProducts.slice(startIndex, endIndex);
  }

  onPageChange(event: PaginatorState): void {
    if (event && event.first !== undefined && event.rows !== undefined) {
      this.currentPage = Math.floor(event.first / event.rows) + 1;
      this.updatePagedProducts();
    }
  }

  trackByProductId(index: number, product: ProductDetailsDtoModel): number {
    return product.id;
  }
}
