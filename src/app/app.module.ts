import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { LoginComponent } from './components/login/login.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { WomenComponent } from './components/women/women.component';
import { MenComponent } from './components/men/men.component';
import { AccessoriesComponent } from './components/accessories/accessories.component';
import { HttpClientModule } from '@angular/common/http';

import { CommentCaMarcheComponent } from './components/comment-ca-marche/comment-ca-marche.component';
import { AddAnnonceComponent } from './components/add-annonce/add-annonce.component';
import { AnnoncesComponent } from './components/annonces/annonces.component';
import { SlickCarouselModule } from 'ngx-slick-carousel';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    LoginComponent,
    WomenComponent,
    MenComponent,
    AccessoriesComponent,
    AddAnnonceComponent,
    HomeComponent,
    AnnoncesComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    SlickCarouselModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
