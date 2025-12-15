import {Routes} from '@angular/router';
import {isConnectedGuard} from './shared/guards/is-connected.guard';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full',
  },
  {
    path: 'home',
    loadComponent: () => import('./features/home/pages/home/home.component').then(m => m.HomeComponent),
  },
  {
    path: 'all-users',
    loadComponent: () => import('./features/user/pages/user-index/user-index.component').then(m => m.UserIndexComponent),
  },
  {
    path: 'customer',
    loadComponent: () => import('./features/customers/pages/customer-index/customer-index.component').then(m => m.CustomerIndexComponent),
    canActivate: [isConnectedGuard]
  },
  {
    path: 'customer/create',
    loadComponent: () => import('./features/customers/pages/customer-create/customer-create.component').then(m => m.CustomerCreateComponent),
    canActivate: [isConnectedGuard]
  },
  {
    path: 'customer/update/:id',
    loadComponent: () => import('./features/customers/components/customer-update/customer-update.component').then(m => m.CustomerUpdateComponent),
    canActivate: [isConnectedGuard]
  },
  {
    path: 'products',
    loadComponent: () =>
      import('./features/products/pages/product-index/product-index.component').then(m => m.ProductIndexComponent),
  },
  {
    path: 'product/create',
    loadComponent: () => import('./features/products/pages/product-create/product-create.component').then(m => m.ProductCreateComponent),
    canActivate: [isConnectedGuard]
  },
  {
    path: 'product/update/:id',
    loadComponent: () => import('./features/products/components/product-update/product-update.component').then(m => m.ProductUpdateComponent),
    canActivate: [isConnectedGuard]
  },
  {
    path: 'invoice/create',
    loadComponent: () => import('./features/invoices/pages/invoice-create/invoice-create.component').then(m => m.InvoiceCreateComponent),
    canActivate: [isConnectedGuard]
  },
  {
    path: 'invoice/card/:id',
    loadComponent: () => import('./features/invoices/components/invoice-card/invoice-card.component').then(m => m.InvoiceCardComponent),
    canActivate: [isConnectedGuard]
  },
  {
    path: 'login',
    loadComponent: () => import('./features/auth/components/login/login.component').then(m => m.LoginComponent),
  },
  {
    path: 'register',
    loadComponent: () => import('./features/auth/components/register/register.component').then(m => m.RegisterComponent),
  },
];
