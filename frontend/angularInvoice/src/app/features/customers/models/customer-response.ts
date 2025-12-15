import {CustomerDetailsDtoModel} from './customer-details-dto.model';

export interface CustomerResponse {
  results: CustomerDetailsDtoModel[];
  totalPages: number;
  currentPage: number;
}
