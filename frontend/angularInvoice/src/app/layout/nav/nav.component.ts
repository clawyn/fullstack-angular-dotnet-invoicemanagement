import { Component, inject, WritableSignal, effect } from '@angular/core';
import { AuthService } from '../../features/auth/services/auth.service';
import { UserTokenDto } from '../../features/auth/models/user-token-dto';
import { MatDialog } from '@angular/material/dialog';
import { RegisterComponent } from '../../features/auth/components/register/register.component';
import { LoginComponent } from '../../features/auth/components/login/login.component';
import { MatMenu, MatMenuItem, MatMenuTrigger } from '@angular/material/menu';
import { MatToolbar } from '@angular/material/toolbar';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  imports: [
    MatIcon,
    MatMenuItem,
    NgIf,
    MatButton,
    MatToolbar,
    MatMenuTrigger,
    MatMenu,
    RouterLink,
  ],
  styleUrls: ['./nav.component.scss']
})
export class NavComponent {
  private readonly _authService: AuthService = inject(AuthService);
  private readonly dialog: MatDialog = inject(MatDialog);

  items: any[] = [];
  currentUser: WritableSignal<UserTokenDto | undefined>;

  constructor() {
    this.currentUser = this._authService.currentUser;
    console.log(this.currentUser());
    effect(() => {
      this.initNav(this.currentUser());
    });
  }

  initNav(currentUser: UserTokenDto | undefined) {
    if (currentUser) {
      this.items = [
        { label: 'Home', icon: 'home', routerLink: '/home' },
        { label: 'Customer', icon: 'customer', routerLink: '/customer' },
        { label: 'Product', icon: 'product', routerLink: '/product' },
        {
          label: currentUser.user.login,
          icon: 'account_circle',
        },
        {
          label: 'Logout',
          icon: 'exit_to_app',
          command: () => this.logout()
        },
      ];
    } else {
      this.items = [
        { label: 'Home', icon: 'home', routerLink: '/home' },
        { label: 'Customer', icon: 'customer', routerLink: '/customer' },
        { label: 'Product', icon: 'product', routerLink: '/product' },
        {
          label: 'Register',
          icon: 'person_add',
          command: () => this.openRegisterDialog(),
        },
        {
          label: 'Login',
          icon: 'login',
          command: () => this.openLoginDialog(),
        }
      ];
    }
  }

  logout() {
    this._authService.logout();
  }

  openRegisterDialog(): void {
    const dialogRef = this.dialog.open(RegisterComponent, {
      width: '400px',
      height: 'auto'
    });

    dialogRef.componentInstance.registrationSuccess.subscribe(() => {
      this.openLoginDialog();
    });
  }


  openLoginDialog(): void {
    this.dialog.open(LoginComponent, {
      width: '400px',
      height: 'auto'
    });
  }
}
