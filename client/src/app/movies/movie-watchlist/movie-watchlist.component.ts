import { Component, inject } from '@angular/core';
import { NgIf } from '@angular/common';
import { MovieCardComponent } from '../movie-card/movie-card.component';
import { CarouselModule } from 'primeng/carousel';
import { DividerModule } from 'primeng/divider';
import { MoviesService } from '../movies.service';
import { toSignal } from '@angular/core/rxjs-interop';
import { Movie } from '../models/movie.model';

@Component({
  selector: 'app-movie-watchlist',
  standalone: true,
  imports: [NgIf, MovieCardComponent, CarouselModule, DividerModule],
  templateUrl: './movie-watchlist.component.html',
  styleUrls: ['./movie-watchlist.component.scss'],
})
export class MovieWatchlistComponent {
  private moviesService = inject(MoviesService);

  watchlistMovies = this.moviesService.watchlistMovies;
  watchlistMovies$ = this.moviesService.watchlistMovies$;

  watchedMovies = this.moviesService.watchedMovies;
  watchedMovies$ = this.moviesService.watchedMovies$;

  //readonly
  watchlistMoviesSub = toSignal(this.watchlistMovies$, {
    initialValue: [] as Movie[],
  });

  //readonly
  watchedMoviesSub = toSignal(this.watchedMovies$, {
    initialValue: [] as Movie[],
  });
}
