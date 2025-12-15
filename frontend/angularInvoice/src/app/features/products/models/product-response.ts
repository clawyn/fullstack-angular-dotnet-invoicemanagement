import {ProductDetailsDtoModel} from './product-details.model';


export interface ProductResponse {
  results: ProductDetailsDtoModel[];
  totalPages: number;
  currentPage: number;
}
