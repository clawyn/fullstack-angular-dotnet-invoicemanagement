import {Component, computed, inject} from '@angular/core';
import {AuthService} from '../../../auth/services/auth.service';
import {NgIf} from '@angular/common';

@Component({
  selector: 'app-home',
  imports: [
    NgIf
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  private authService = inject(AuthService);

  ngOnInit(): void {
    console.log('Utilisateur connecté :', this.authService.currentUser());
  }

  userLogin = computed(() => this.authService.currentUser()?.user.login ?? '');
}
