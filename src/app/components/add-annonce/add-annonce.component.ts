import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Annonce, AnnonceService } from '../../services/annonce.service';

@Component({
  selector: 'app-add-annonce',
  standalone: false,
  templateUrl: './add-annonce.component.html',
  styleUrl: './add-annonce.component.css'
})

export class AddAnnonceComponent {
  annonce: Annonce = {
    id: 0,
    titre: '',
    description: '',
    prix: 0,
    datePublication: '',
    utilisateurId: 0,
    utilisateurNom: '',
    imagesUrls: [] // ✅ Correction ici
  };

  constructor(private annonceService: AnnonceService) {}

  onSubmit() {
    this.annonceService.addAnnonce(this.annonce).subscribe({
      next: (res) => {
        alert('Annonce ajoutée avec succès !');
        this.annonce = {
          id: 0,
          titre: '',
          description: '',
          prix: 0,
          datePublication: '',
          utilisateurId: 0,
          utilisateurNom: '',
          imagesUrls: [] // ✅ Réinitialisation
        };
      },
      error: (err) => {
        console.error('Erreur:', err);
        alert('Erreur lors de l\'ajout');
      }
    });
  }
}