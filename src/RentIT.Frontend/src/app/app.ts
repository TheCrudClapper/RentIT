import { Component, signal } from '@angular/core';
import { Navbar } from './components/navbar/navbar/navbar';
import { Footer } from './components/footer/footer/footer';
import { Login } from './components/auth/login/login/login';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  imports: [Navbar, Footer, Login],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('RentIT.Frontend');
}
