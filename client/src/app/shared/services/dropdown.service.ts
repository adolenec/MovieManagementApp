import { HttpClient } from '@angular/common/http';
import { Injectable, inject, signal } from '@angular/core';
import { toObservable, toSignal } from '@angular/core/rxjs-interop';
import { debounceTime, switchMap } from 'rxjs';
import { API_URL } from 'src/app/app.config';
import { Dropdown } from '../models/dropdown.model';

@Injectable({
  providedIn: 'root',
})
export class DropdownService {
  private http = inject(HttpClient);
  private apiUrl = inject(API_URL);

  categoryDropdownRequest = signal('');

  private categoriesDropdown$ = toObservable(this.categoryDropdownRequest).pipe(
    debounceTime(200),
    switchMap((searchTerm) => {
      return this.http.get<Dropdown[]>(
        `${this.apiUrl}/categories/dropdown/?searchTerm=${searchTerm}`
      );
    })
  );

  categoriesDropdown = toSignal(this.categoriesDropdown$, {
    initialValue: [] as Dropdown[],
  });

  directorsDropdownRequest = signal('');

  private directorsDropdown$ = toObservable(this.directorsDropdownRequest).pipe(
    debounceTime(200),
    switchMap((searchTerm) => {
      return this.http.get<Dropdown[]>(
        `${this.apiUrl}/directors/dropdown/?searchTerm=${searchTerm}`
      );
    })
  );

  directorsDropdown = toSignal(this.directorsDropdown$, {
    initialValue: [] as Dropdown[],
  });
}
