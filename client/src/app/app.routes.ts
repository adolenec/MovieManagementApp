import { Routes } from '@angular/router';

export const APP_ROUTES: Routes = [
  { path: '', redirectTo: 'movies', pathMatch: 'full' },
  {
    path: 'movies',
    loadChildren: () =>
      import('./movies/movies.routes').then((r) => r.MOVIES_ROUTES),
  },
  {
    path: 'directors',
    loadChildren: () =>
      import('./directors/directors.routes').then((r) => r.DIRECTORS_ROUTES),
  },
  {
    path: 'categories',
    loadChildren: () =>
      import('./categories/categories.routes').then((r) => r.CATEGORIES_ROUTES),
  },
];
