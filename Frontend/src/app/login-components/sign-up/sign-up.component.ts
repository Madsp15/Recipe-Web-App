import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {IonicModule} from "@ionic/angular";
import {Router} from "@angular/router";

@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [CommonModule, IonicModule],
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.css'
})
export class SignUpComponent {
  showPassword: boolean = false;
  constructor(private router: Router){}

  clickJoin() {

  }

  clickBack() {
    this.router.navigate(['login']);
  }

  togglePasswordVisibility() {

  }
}
