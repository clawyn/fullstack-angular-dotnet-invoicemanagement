import {Component, EventEmitter, inject, Input, Output} from '@angular/core';
import {Card} from 'primeng/card';
import {Button, ButtonDirective} from 'primeng/button';
import {ActivatedRoute, Router, RouterLink} from '@angular/router';
import {InvoiceService} from '../../service/invoice.service';
import {InvoiceDetailsDtoModel, InvoiceProductDetailsDtoModel} from '../../models/invoice-details.model';
import {NgForOf, NgIf} from '@angular/common';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ProductDetailsDtoModel} from '../../../products/models/product-details.model';
import {ProductService} from '../../../products/services/product.service';
import {InvoiceResponse} from '../../models/invoice-respond';


@Component({
  selector: 'app-invoice-card',
  imports: [
    Card,
    NgForOf,
    NgIf,
    ButtonDirective,
  ],
  templateUrl: './invoice-card.component.html',
  styleUrl: './invoice-card.component.scss'
})
export class InvoiceCardComponent {
  @Input({required: true})
  invoice!: InvoiceDetailsDtoModel;
  product!: ProductDetailsDtoModel;
  invoiceIdString!: string;
  invoices!: InvoiceResponse;
  productDetailsMap: { [productId: string]: ProductDetailsDtoModel } = {};



  constructor(
    private readonly _fb: FormBuilder = inject(FormBuilder),
    private readonly _invoiceService: InvoiceService = inject(InvoiceService),
    private readonly _productService: ProductService = inject(ProductService),
    private readonly _router: Router = inject(Router),
    private readonly _route: ActivatedRoute,
  ) {
  }

  @Output() invoiceDeleted = new EventEmitter<void>();
  public price!: number;


  deleteInvoice(invoiceId: number): void {
    if (confirm('Are you sure you want to delete this invoice ?')) {
      this._invoiceService.delete(invoiceId).subscribe({
        next: () => {
          console.log('Invoice deleted successfully');
          this._router.navigate(['/customer']);

          this.invoiceDeleted.emit();
        },
        error: (err) => {
          console.error('Error deleting invoice', err);
        }
      });
    }
  }

  getTotalAmount(): number {
    return this.invoice.invoiceProducts.reduce((sum, p) => sum + (p.amount || 0), 0);
  }

  ngOnInit(): void {
    const id = this._route.snapshot.paramMap.get('id');
    if (id) {
      this._invoiceService.getInvoiceByIdString(id).subscribe({
        next: (data) => {
          this.invoice = data;
          this.loadProductDetails();
        },
        error: (err) => {
          console.error('Erreur lors de la récupération de la facture :', err);
        }
      });
    }
  }
  loadProductDetails(): void {
    for (const product of this.invoice.invoiceProducts) {
      console.log('productId récupéré depuis l’URL :', product.productId);
      if (product.productId) {
        this._productService.getProductByIdString(product.productId).subscribe({
          next: (productData) => {
            console.log('Produit récupéré :', productData);
            this.price = productData.price
            this.productDetailsMap[product.productId] = productData;
          },
          error: (err) => {
            console.error('Erreur lors de la récupération du produit :', err);
          },
        });
      }
    }
  }
}

