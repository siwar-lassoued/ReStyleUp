import { Component, AfterViewInit,OnInit, Inject } from '@angular/core';
import { Annonce, AnnonceService } from '../../services/annonce.service';
declare var $: any;

@Component({
  selector: 'app-home',
  standalone: false,
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
   
})
export class HomeComponent implements AfterViewInit, OnInit {
  
  annonces: Annonce[] = [];

  constructor(@Inject(AnnonceService) private annonceService: AnnonceService) {}

  ngOnInit(): void {
    this.annonceService.getAnnonces().subscribe(data => {
      this.annonces = data;
    });
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
