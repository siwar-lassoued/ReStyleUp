import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

// Interface article (à adapter selon ton modèle)
export interface Article {
  id: string;
  categorie: string;
  nom: string;
  description: string;
  prix: number;
  etat: string;
  annonceId: string;
  annonceTitre: string
  imageUrl:string
}

@Injectable({
  providedIn: 'root', 
 
})
export class ArticleService {

  private apiUrl = 'http://localhost:5117/api/article'; // change par ton url API

  constructor(private http: HttpClient) {}

  // Récupérer tous les articles
  getArticles(): Observable<Article[]> {
    return this.http.get<Article[]>(this.apiUrl);
  }

  // Récupérer un article par id
  getArticleById(id: number): Observable<Article> {
    return this.http.get<Article>(`${this.apiUrl}/${id}`);
  }

  // Créer un nouvel article
  createArticle(article: Article): Observable<Article> {
    return this.http.post<Article>(this.apiUrl, article);
  }

  // Mettre à jour un article
  updateArticle(id: number, article: Article): Observable<Article> {
    return this.http.put<Article>(`${this.apiUrl}/${id}`, article);
  }

  // Supprimer un article
  deleteArticle(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
