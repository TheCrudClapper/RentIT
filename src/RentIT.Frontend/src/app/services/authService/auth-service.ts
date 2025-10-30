import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RegisterRequest } from '../../models/auth/RegisterRequest';
import { LoginRequest } from '../../models/auth/LoginRequest';


@Injectable({
  providedIn: 'root'
})

export class AuthService {
  private registerUrl = "http://localhost:5050/gateway/auth/register";
  private loginUrl = "http://localhost:5050/gateway/auth/login";

  constructor(private http: HttpClient){}

  register(payload: RegisterRequest): Observable<any> {
    return this.http.post(this.registerUrl, payload);
  }
  login(payload: LoginRequest): Observable<any>{
    return this.http.post(this.loginUrl, payload);
  }
}
