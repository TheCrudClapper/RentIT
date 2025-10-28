import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Home } from './components/home/home';
import { Navbar } from './components/navbar/navbar/navbar';
import { Footer } from './components/footer/footer/footer';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Home, Navbar, Footer],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('RentIT.Frontend');
}
