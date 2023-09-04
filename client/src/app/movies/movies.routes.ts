import { Routes } from '@angular/router';
import { MoviesComponent } from './movies.component';
import { MovieDetailsComponent } from './movie-details/movie-details.component';

export const MOVIES_ROUTES: Routes = [
  { path: '', component: MoviesComponent },
  { path: ':id', component: MovieDetailsComponent },
];
