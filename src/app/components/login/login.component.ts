import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginForm: FormGroup;

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
    
  }

  onSubmit() {
    this.authService.login(this.loginForm.value).subscribe({
      next: (res: any) => {
        const token = res.token; // maintenant reconnu
        localStorage.setItem('token', token);
        alert('Connexion réussie ✅');
        this.router.navigate(['/']);
      },
      error: (err) => {
        console.error(err);
        alert('Échec de la connexion ❌');
      }
    });
    
  }
}
