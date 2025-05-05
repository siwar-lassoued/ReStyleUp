import { Component, OnInit } from '@angular/core';
import { Annonce, AnnonceService } from '../../services/annonce.service';

@Component({
  selector: 'app-annonces',
  standalone: false,
  templateUrl: './annonces.component.html',
  styleUrl: './annonces.component.css'
})
export class AnnoncesComponent implements OnInit {
  annonces = [
    {
      id: 1,
      titre: 'Veste en jean',
      description: 'Veste en jean bleue taille M',
      prix: 50,
      imagesUrls: ['assets/images/product-04.jpg', 'assets/images/product-04.jpg']
    },
    {
      id: 2,
      titre: 'Robe été',
      description: 'Robe légère pour l\'été taille S',
      prix: 30,
      imagesUrls: ['assets/images/product-04.jpg']
    }
  ];

  constructor() {}

  ngOnInit(): void {}
}
