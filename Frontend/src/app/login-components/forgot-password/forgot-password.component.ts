import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {IonicModule} from "@ionic/angular";
import {Router} from "@angular/router";

@Component({
  selector: 'app-forgot-password',
  standalone: true,
  imports: [CommonModule, IonicModule],
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.css'
})
export class ForgotPasswordComponent {

  constructor(private router: Router) {
  }

  clickBack() {
    this.router.navigate(['login']);
  }

  clickSend() {

  }
}
