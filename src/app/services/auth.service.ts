// src/app/services/auth.service.ts
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

export interface RegisterModel {
  username: string;
  email: string;
  password: string;
  role?: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = 'https://localhost:7249/api/auth'; // Adapté à ta config

  constructor(private http: HttpClient) {}

  register(user: RegisterModel) {
    return this.http.post(`${this.baseUrl}/register`, user);
  }

  login(credentials: { username: string; password: string }) {
    return this.http.post(`${this.baseUrl}/login`, credentials);
  }
}
