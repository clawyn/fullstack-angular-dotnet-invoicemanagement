import {CanActivateFn, Router} from '@angular/router';
import {AuthService} from '../../features/auth/services/auth.service';
import {inject} from '@angular/core';

export const isAdminGuard: CanActivateFn = (route, state) => {
  const authService: AuthService = inject(AuthService);
  const router: Router = inject(Router);

  const user = authService.currentUser();

  if (!user || user.user.login == undefined) {
    router.navigate(['/home']);
    return false;
  }

  return true;
};
