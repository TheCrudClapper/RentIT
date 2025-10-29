import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Home } from './components/home/home';
import { Navbar } from './components/navbar/navbar/navbar';
import { Footer } from './components/footer/footer/footer';
import { EquipmentPage } from "./components/equipment/equipment-page/equipment-page";
import { Login } from "./components/auth/login/login/login";
import { Register } from "./components/auth/login/register/register/register";

@Component({
  selector: 'app-root',
  imports: [Navbar, Footer,Register],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('RentIT.Frontend');
}
