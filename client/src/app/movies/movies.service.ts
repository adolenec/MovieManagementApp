import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject, signal } from '@angular/core';
import { API_URL } from '../app.config';
import { toObservable, toSignal } from '@angular/core/rxjs-interop';
import { debounceTime, switchMap, tap } from 'rxjs';
import { PagedList } from '../shared/models/paged-list.model';
import { MoviesRequest } from './models/movies-request.model';
import { Movie } from './models/movie.model';
import { MovieDetails } from './models/movie-details.model';

@Injectable({
  providedIn: 'root',
})
export class MoviesService {
  private http = inject(HttpClient);
  private apiUrl = inject(API_URL);

  moviesRequest = signal<MoviesRequest>({
    page: 1,
    pageSize: 5,
  });
  loadingMovies = signal(true);

  private movies$ = toObservable(this.moviesRequest).pipe(
    debounceTime(200),
    switchMap((req) => {
      this.loadingMovies.set(true);
      const params = new HttpParams({ fromObject: { ...req } });
      return this.http.get<PagedList<Movie>>(`${this.apiUrl}/movies`, {
        params,
      });
    }),
    tap((_) => this.loadingMovies.set(false))
  );

  movies = toSignal(this.movies$, {
    initialValue: {} as PagedList<Movie>,
  });

  selectedMovieId = signal<number | undefined>(undefined);
  movieDetails = signal<MovieDetails>({} as MovieDetails);
  movieDetails$ = toObservable(this.selectedMovieId).pipe(
    switchMap((id) => {
      return this.http.get<MovieDetails>(`${this.apiUrl}/movies/${id}`);
    }),
    tap((movie) => this.movieDetails.set(movie))
  );

  //refetch but keep paging and searching params
  refetchMovies() {
    this.moviesRequest.set({
      ...this.moviesRequest(),
    });
  }

  updateFavouriteState(id: number, isFavourite: boolean) {
    return this.http.put<void>(
      `${this.apiUrl}/movies/${id}/favourite`,
      isFavourite
    );
  }

  updateWatchedState(id: number, watched: boolean) {
    return this.http.put<void>(`${this.apiUrl}/movies/${id}/watched`, watched);
  }

  updateWatchlistState(id: number, isOnWatchlist: boolean) {
    return this.http.put<void>(
      `${this.apiUrl}/movies/${id}/watchlist`,
      isOnWatchlist
    );
  }

  favouriteMovies = signal<Movie[]>([] as Movie[]);
  favouriteMovies$ = this.http
    .get<Movie[]>(`${this.apiUrl}/movies/favourites`)
    .pipe(tap((movies) => this.favouriteMovies.set(movies)));

  watchedMovies = signal<Movie[]>([] as Movie[]);
  watchedMovies$ = this.http
    .get<Movie[]>(`${this.apiUrl}/movies/watched`)
    .pipe(tap((movies) => this.watchedMovies.set(movies)));

  watchlistMovies = signal<Movie[]>([] as Movie[]);
  watchlistMovies$ = this.http
    .get<Movie[]>(`${this.apiUrl}/movies/watchlist`)
    .pipe(tap((movies) => this.watchlistMovies.set(movies)));
}
