import {Component, EventEmitter, inject, Input, Output} from '@angular/core';
import {Card} from 'primeng/card';
import {Button, ButtonDirective} from 'primeng/button';
import {CustomerResponse} from '../../models/customer-response';
import {CustomerService} from '../../services/customer.service';
import {Router, RouterLink} from '@angular/router';
import {CustomerShortDtoModel} from '../../models/customer-short-dto.model';
import {InvoiceDetailsDtoModel} from '../../../invoices/models/invoice-details.model';
import {InvoiceService} from '../../../invoices/service/invoice.service';
import {NgForOf, NgIf} from '@angular/common';

@Component({
  selector: 'app-customer-card',
  imports: [
    Card,
    ButtonDirective,
    NgIf,
    NgForOf,
    Button,
    RouterLink,
  ],
  templateUrl: './customer-card.component.html',
  styleUrl: './customer-card.component.scss'
})
export class CustomerCardComponent {
  @Input({required: true})
  customer!: CustomerShortDtoModel;
  customers!: CustomerResponse;
  invoicesMap: { [customerId: string]: InvoiceDetailsDtoModel[] } = {};

  constructor(
    private readonly _customerService: CustomerService = inject(CustomerService),
    private readonly _invoiceService: InvoiceService = inject(InvoiceService),
    private readonly _router: Router = inject(Router)
  ) {
  }

  @Output() customerDeleted = new EventEmitter<void>();


  deleteCustomer(customerId: number): void {
    if (confirm('Are you sure you want to delete this customer ?')) {
      this._customerService.delete(customerId).subscribe({
        next: () => {
          console.log('Customer deleted successfully');

          this.customerDeleted.emit();
        },
        error: (err) => {
          console.error('Error deleting customer', err);
        }
      });
    }
  }



  update(customerId: number): void {
    console.log('Updating customer with ID:', customerId);
    this._router.navigate([`/customer/update/${customerId}`]);
  }

  toggleInvoices(customerId: string): void {
    console.log('Customer ID envoyé au backend:', customerId); // Ajout du log ici

    if (this.invoicesMap[customerId]) {
      delete this.invoicesMap[customerId]; // toggle off
    } else {
      this._invoiceService.getInvoicesByCustomerId(customerId).subscribe({
        next: (invoices) => {
          console.log('Réponse reçue du backend:', invoices); // Log de la réponse du backend
          this.invoicesMap[customerId] = invoices;
        },
        error: (err) => {
          console.error('Erreur lors du chargement des invoices du client:', err);
          alert(`Erreur: ${err.error.message || 'Une erreur est survenue lors du chargement des invoices'}`);
        }
      });
    }
  }
}
