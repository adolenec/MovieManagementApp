import { Component, inject } from '@angular/core';
import { NgIf } from '@angular/common';
import { MoviesService } from '../movies.service';
import { toSignal } from '@angular/core/rxjs-interop';
import { Movie } from '../models/movie.model';
import { CarouselModule } from 'primeng/carousel';
import { DividerModule } from 'primeng/divider';
import { MovieCardComponent } from '../movie-card/movie-card.component';

@Component({
  selector: 'app-movie-favourites',
  standalone: true,
  imports: [NgIf, MovieCardComponent, CarouselModule, DividerModule],
  templateUrl: './movie-favourites.component.html',
  styleUrls: ['./movie-favourites.component.scss'],
})
export class MovieFavouritesComponent {
  private moviesService = inject(MoviesService);

  favouriteMovies = this.moviesService.favouriteMovies;
  favouriteMovies$ = this.moviesService.favouriteMovies$;

  //readonly
  favouriteMoviesSub = toSignal(this.favouriteMovies$, {
    initialValue: [] as Movie[],
  });
}
