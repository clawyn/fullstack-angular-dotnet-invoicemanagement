import {InvoiceDetailsDtoModel} from './invoice-details.model';

export interface InvoiceResponse {
  results: InvoiceDetailsDtoModel[];
  totalPages: number;
  currentPage: number;
}
