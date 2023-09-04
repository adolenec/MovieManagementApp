import { Routes } from '@angular/router';
import { DirectorsComponent } from './directors.component';
import { DirectorDetailsComponent } from './director-details/director-details.component';

export const DIRECTORS_ROUTES: Routes = [
  { path: '', component: DirectorsComponent },
  { path: ':id', component: DirectorDetailsComponent },
];
