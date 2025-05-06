import { Component } from '@angular/core';
import { FormBuilder, Validators, FormGroup, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-signup',
  standalone: true,
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
  imports: [CommonModule, ReactiveFormsModule, FormsModule]
})

export class SignupComponent {
  signupForm: FormGroup;

  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router) {
    this.signupForm = this.fb.group({
      username: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
      role:['',Validators.required]
    });
  }

  submitForm() {
    if (this.signupForm.invalid) {
      alert('Form is invalid'); // ← ça aide à comprendre si rien ne se passe
      return;
    }
    
    if (this.signupForm.invalid) return;

    const { username, email, password, confirmPassword , role} = this.signupForm.value;

    if (password !== confirmPassword) {
      alert('Passwords do not match');
      return;
    }

    const payload = {
      username,
      email,
      password,
      role: role || 'User'  // Si aucun rôle n'est spécifié, attribuer 'User' par défaut
    };

    this.http.post('/api/auth/register', payload, { responseType: 'text' }).subscribe({
      next: (res) => {
        console.log('Success response:', res);
        alert(res); // ← affichera "User registered with role 'User'"
        this.router.navigate(['/login']);
      },
      error: (err) => {
        console.error('Error response:', err);
        alert('Registration failed');
      }
    });
    
    
  }
}
