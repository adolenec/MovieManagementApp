import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject, signal } from '@angular/core';
import { API_URL } from '../app.config';
import { DirectorsRequest } from './models/directors-request.model';
import { toSignal, toObservable } from '@angular/core/rxjs-interop';
import { debounceTime, switchMap, tap } from 'rxjs';
import { PagedList } from '../shared/models/paged-list.model';
import { Director } from './models/director.model';
import { DirectorDetails } from './models/director-details.model';

@Injectable({
  providedIn: 'root',
})
export class DirectorsService {
  private http = inject(HttpClient);
  private apiUrl = inject(API_URL);

  directorsRequest = signal<DirectorsRequest>({
    page: 1,
    pageSize: 5,
  });
  loadingDirectors = signal(true);

  private directors$ = toObservable(this.directorsRequest).pipe(
    debounceTime(200),
    switchMap((req) => {
      this.loadingDirectors.set(true);
      const params = new HttpParams({ fromObject: { ...req } });
      return this.http.get<PagedList<Director>>(`${this.apiUrl}/directors`, {
        params,
      });
    }),
    tap((_) => this.loadingDirectors.set(false))
  );

  directors = toSignal(this.directors$, {
    initialValue: {} as PagedList<Director>,
  });

  selectedDirectorId = signal<number | undefined>(undefined);
  directorDetails$ = toObservable(this.selectedDirectorId).pipe(
    switchMap((id) => {
      return this.http.get<DirectorDetails>(`${this.apiUrl}/directors/${id}`);
    })
  );

  //refetch but keep paging and searching params
  refetchDirectors() {
    this.directorsRequest.set({
      ...this.directorsRequest(),
    });
  }

  deleteDirector(id: number) {
    return this.http.delete<void>(`${this.apiUrl}/directors/${id}`);
  }

  createDirector(directorDetails: DirectorDetails) {
    return this.http.post<DirectorDetails>(
      `${this.apiUrl}/directors`,
      directorDetails
    );
  }

  updateDirector(directorDetails: DirectorDetails, id: number) {
    return this.http.put<DirectorDetails>(
      `${this.apiUrl}/directors/${id}`,
      directorDetails
    );
  }
}
