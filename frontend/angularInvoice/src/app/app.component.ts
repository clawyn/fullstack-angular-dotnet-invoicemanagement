import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {NavComponent} from './layout/nav/nav.component';
import {FooterComponent} from './layout/footer/footer.component';

function HeaderComponent() {

}

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NavComponent, FooterComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'angularInvoice';
}
