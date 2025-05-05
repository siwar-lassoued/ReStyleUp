import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { WomenComponent } from './components/women/women.component';
import { MenComponent } from './components/men/men.component';
import { AccessoriesComponent } from './components/accessories/accessories.component';
import { CommentCaMarcheComponent } from './components/comment-ca-marche/comment-ca-marche.component';
import { AddAnnonceComponent } from './components/add-annonce/add-annonce.component';
import { AnnoncesComponent } from './components/annonces/annonces.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'women', component: WomenComponent },
  { path: 'men', component: MenComponent },
  { path: 'accessories', component: AccessoriesComponent },
  { path: 'home', component: HomeComponent },
  {path : 'login' , component : LoginComponent},
  {path : 'signup' , component : SignupComponent},
  { path: 'comment-ca-marche', component: CommentCaMarcheComponent },
  { path: 'annonce', component: AddAnnonceComponent },
  {path: 'annonces', component: AnnoncesComponent} // Redirige vers la page d'accueil pour toute autre route non définie



];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
