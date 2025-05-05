import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-comment-ca-marche',
  templateUrl: './comment-ca-marche.component.html',
  styleUrls: ['./comment-ca-marche.component.css'],
  imports: [CommonModule]  // ✅ Ajouter ceci
})
export class CommentCaMarcheComponent {
  etapes = [
    { titre: 'Étape 1', description: 'Crée ton compte facilement.' },
    { titre: 'Étape 2', description: 'Prends des photos de tes articles.' },
    { titre: 'Étape 3', description: 'Mets-les en vente en quelques clics.' },
    { titre: 'Étape 4', description: 'Achète ou vends, et profites-en !' }
  ];
}
