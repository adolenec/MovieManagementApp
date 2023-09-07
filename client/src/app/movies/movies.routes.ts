import { Routes } from '@angular/router';
import { MoviesComponent } from './movies.component';
import { MovieDetailsComponent } from './movie-details/movie-details.component';
import { MovieWatchlistComponent } from './movie-watchlist/movie-watchlist.component';
import { MovieFavouritesComponent } from './movie-favourites/movie-favourites.component';
import { MovieLayoutComponent } from './movie-layout/movie-layout.component';

export const MOVIES_ROUTES: Routes = [
  {
    path: '',
    component: MovieLayoutComponent,
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'all',
      },
      { path: 'all', component: MoviesComponent },
      { path: 'watchlist', component: MovieWatchlistComponent },
      { path: 'favourites', component: MovieFavouritesComponent },
    ],
  },
  { path: ':id', component: MovieDetailsComponent },
];
