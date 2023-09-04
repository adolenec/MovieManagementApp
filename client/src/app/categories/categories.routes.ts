import { Routes } from '@angular/router';
import { CategoriesComponent } from '../categories/categories.component';
import { CategoryDetailsComponent } from './category-details/category-details.component';

export const CATEGORIES_ROUTES: Routes = [
  { path: '', component: CategoriesComponent },
  { path: ':id', component: CategoryDetailsComponent },
];
