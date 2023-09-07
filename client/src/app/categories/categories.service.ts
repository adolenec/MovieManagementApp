import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject, signal } from '@angular/core';
import { API_URL } from '../app.config';
import { CategoriesRequest } from './models/categories-request.model';
import { toObservable, toSignal } from '@angular/core/rxjs-interop';
import { debounceTime, switchMap, tap } from 'rxjs';
import { PagedList } from '../shared/models/paged-list.model';
import { Category } from './models/category.model';
import { CategoryDetails } from './models/category-details.model';

@Injectable({
  providedIn: 'root',
})
export class CategoriesService {
  private http = inject(HttpClient);
  private apiUrl = inject(API_URL);

  categoriesRequest = signal<CategoriesRequest>({
    page: 1,
    pageSize: 5,
  });
  loadingCategories = signal(true);

  private categories$ = toObservable(this.categoriesRequest).pipe(
    debounceTime(200),
    switchMap((req) => {
      this.loadingCategories.set(true);
      const params = new HttpParams({ fromObject: { ...req } });
      return this.http.get<PagedList<Category>>(`${this.apiUrl}/categories`, {
        params,
      });
    }),
    tap((_) => this.loadingCategories.set(false))
  );

  categories = toSignal(this.categories$, {
    initialValue: {} as PagedList<Category>,
  });

  selectedCategoryId = signal<number | undefined>(undefined);
  categoryDetails$ = toObservable(this.selectedCategoryId).pipe(
    switchMap((id) => {
      return this.http.get<CategoryDetails>(`${this.apiUrl}/categories/${id}`);
    })
  );

  //refetch but keep paging and searching params
  refetchCategories() {
    this.categoriesRequest.set({
      ...this.categoriesRequest(),
    });
  }

  deleteCategory(id: number) {
    return this.http.delete<void>(`${this.apiUrl}/categories/${id}`);
  }

  createCategory(categoryDetails: CategoryDetails) {
    return this.http.post<CategoryDetails>(
      `${this.apiUrl}/categories`,
      categoryDetails
    );
  }

  updateCategory(categoryDetails: CategoryDetails, id: number) {
    return this.http.put<CategoryDetails>(
      `${this.apiUrl}/categories/${id}`,
      categoryDetails
    );
  }
}
