import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../../../services/authService/auth-service';
import { LoginRequest } from '../../../../models/auth/LoginRequest';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login {

  email: string = "";
  password: string = "";

  constructor(private authService: AuthService) { };

  login() {
    const loginPayload: LoginRequest = {
      email: this.email,
      password: this.password
    }

    

  };
}
