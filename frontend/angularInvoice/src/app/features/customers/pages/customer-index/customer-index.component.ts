import {Component, computed, inject, Input, Signal, WritableSignal} from '@angular/core';

import {Button} from 'primeng/button';
import {RouterLink} from '@angular/router';
import { TableModule } from 'primeng/table';

import {Paginator, PaginatorState} from 'primeng/paginator';
import {AuthService} from '../../../auth/services/auth.service';
import {CustomerDetailsDtoModel} from '../../models/customer-details-dto.model';
import {UserTokenDto} from '../../../auth/models/user-token-dto';
import {CustomerService} from '../../services/customer.service';
import {CustomerCardComponent} from '../../components/customer-card/customer-card.component';


@Component({
  selector: 'app-customer-index',
  imports: [
    Button,
    RouterLink,
    TableModule,
    CustomerCardComponent,
    Paginator,
  ],
  templateUrl: './customer-index.component.html',
  styleUrl: './customer-index.component.scss'
})
export class CustomerIndexComponent {
  private readonly _customerService: CustomerService = inject(CustomerService);
  private readonly _authService: AuthService = inject(AuthService);

  @Input()
  customers: CustomerDetailsDtoModel[] = [] as CustomerDetailsDtoModel[];
  allCustomers: CustomerDetailsDtoModel[] = [];

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
    this.loadCustomers();
  }

  loadCustomers() {
    this._customerService.getAll().subscribe({
      next: (data: CustomerDetailsDtoModel[]) => {
        this.allCustomers = data;
        this.totalPages = Math.ceil(this.allCustomers.length / this.pageSize);
        this.updatePagedCustomers();
      },
      error: (err) => console.log(err),
    });
  }

  updatePagedCustomers() {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    this.customers = this.allCustomers.slice(startIndex, endIndex);
  }

  onPageChange(event: PaginatorState): void {
    if (event && event.first !== undefined && event.rows !== undefined) {
      this.currentPage = Math.floor(event.first / event.rows) + 1;
      this.updatePagedCustomers();
    }
  }

  trackByCustomerId(index: number, customer: CustomerDetailsDtoModel): number {
    return customer.id;
  }
}
