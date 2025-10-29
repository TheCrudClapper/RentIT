import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface RegisterPayload{
  firstName: string;
  lastName: string;
  email: string;
  password: string;
}

export interface LoginPayload{
  email: string;
  password: string;
}

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  private registerUrl = "http://localhost:5050/gateway/auth/register";
  private loginUrl = "http://localhost:5050/gateway/auth/register";

  constructor(private http: HttpClient){}

  register(payload: RegisterPayload): Observable<any> {
    return this.http.post(this.registerUrl, payload);
  }
  login(payload: LoginPayload): Observable<any>{
    return this.http.post(this.loginUrl, payload);
  }
}
