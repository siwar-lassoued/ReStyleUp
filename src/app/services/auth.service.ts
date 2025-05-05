import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor() { }

  isLoggedIn(): boolean {
    // Simule la vérification : tu peux adapter selon ton système d'authentification réel
    return !!localStorage.getItem('userToken'); // par exemple, tu stockes un token à la connexion
  }

  login(token: string): void {
    localStorage.setItem('userToken', token);
  }

  logout(): void {
    localStorage.removeItem('userToken');
  }
}
