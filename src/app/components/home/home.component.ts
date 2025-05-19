import { Component, AfterViewInit,OnInit, Inject } from '@angular/core';
import { Annonce, AnnonceService } from '../../services/annonce.service';
import { Article, ArticleService } from '../../services/article.service';
declare var $: any;

@Component({
  selector: 'app-home',
  standalone: false,
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
   
})
export class HomeComponent implements AfterViewInit, OnInit {
  
  // annonces: Annonce[] = [];
  // articles: Article[] = [];

constructor(
  // @Inject(AnnonceService) private annonceService: AnnonceService,
  // @Inject(ArticleService) private articleService: ArticleService
) {}

  ngOnInit(): void {
  // this.annonceService.getAnnonces().subscribe(data => {
  //   this.annonces = data;
  // });

  // this.articleService.getArticles().subscribe(data => {
  //   this.articles = data;
  // });
}

  showModal = false;

openModal() {
  this.showModal = true;
}

closeModal() {
  this.showModal = false;
}


  ngAfterViewInit(): void {
    // Initialisation de Slick
    $('.slick1').slick({
      slidesToShow: 1,
      slidesToScroll: 1,
      fade: true,
      dots: true,
      arrows: true,
      autoplay: true,
      autoplaySpeed: 1000
    });

  }
}
