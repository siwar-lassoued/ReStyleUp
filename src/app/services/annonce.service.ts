import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';


export interface Annonce {
  id: number;
  titre: string;
  description: string;
  prix: number;
  datePublication: string;
  utilisateurId: number;
  utilisateurNom: string;
  imagesUrls: string[];
}
@Injectable({
  providedIn: 'root'
})
export class AnnonceService {
  
  private apiUrl = 'http://localhost:5117/api/annonce'; // adapte le port si besoin

  constructor(private http: HttpClient) {}

  getAnnonces(): Observable<Annonce[]> {
    return this.http.get<Annonce[]>(this.apiUrl);
    
  }

  addAnnonce(annonce: Annonce): Observable<Annonce> {
    return this.http.post<Annonce>(this.apiUrl, annonce);
  }
  
}
