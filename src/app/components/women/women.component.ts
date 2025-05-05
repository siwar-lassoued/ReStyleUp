import { Component } from '@angular/core';

@Component({
  selector: 'app-women',
  standalone: false,
  templateUrl: './women.component.html',
  styleUrl: './women.component.css'
})
export class WomenComponent {

  products = [
    { name: 'Robe été', price: 70, img: 'assets/images/women1.jpg' },
    { name: 'Sac à main', price: 120, img: 'assets/images/women2.jpg' }
  ];
}
