export interface InvoiceDetailsDtoModel {
  id: number;
  name: string;
  description: string;
  workForce: string;
  customerId: string;
  invoiceProducts: InvoiceProductDetailsDtoModel[];
}

export interface InvoiceProductDetailsDtoModel {
  productId: string;
  productName: string;
  quantity: number;
  price: number;
  amount: number;
  products: {
    productId: string;
    price: number;
    quantity: number;
  }[];
}
