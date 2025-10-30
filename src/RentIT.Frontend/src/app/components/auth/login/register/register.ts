import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../../../services/authService/auth-service';
import { RegisterRequest } from '../../../../models/auth/RegisterRequest';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.html',
  styleUrl: './register.scss',
})
export class Register {
  firstName: string = "";
  lastName: string = "";
  email: string = "";
  password: string = "";

  constructor(private authService: AuthService) { }

  register() {
    const payload: RegisterRequest = {
      firstName: this.firstName,
      lastName: this.lastName,
      email: this.email,
      password: this.password
    };

    this.authService.register(payload).subscribe({
      next: (response) => console.log("Success", response),
      error: (err) => console.error("Error", err)
    });
  }
}
