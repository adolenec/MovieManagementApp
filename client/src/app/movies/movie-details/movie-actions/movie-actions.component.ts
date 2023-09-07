import { Component, inject } from '@angular/core';
import { NgClass } from '@angular/common';
import { MoviesService } from '../../movies.service';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-movie-actions',
  standalone: true,
  imports: [NgClass],
  templateUrl: './movie-actions.component.html',
})
export class MovieActionsComponent {
  private moviesService = inject(MoviesService);
  private messageService = inject(MessageService);

  movieDetails = this.moviesService.movieDetails;

  onUpdateFavouriteState(state: boolean) {
    const summary = state ? 'Added to favourites' : 'Removed from favourites';
    const detail = state
      ? 'You have successfully added a movie to favourites!'
      : 'You have successfully removed a movie from favourites!';
    this.moviesService
      .updateFavouriteState(this.movieDetails().id, state)
      .subscribe((_) => {
        this.movieDetails.mutate((movie) => (movie.isFavourite = state));
        this.moviesService.refetchMovies();
        this.messageService.add({
          summary,
          detail,
        });
      });
  }

  onUpdateWatchedState(state: boolean) {
    const summary = state ? 'Marked as watched' : 'Marked as not watched';
    const detail = state
      ? 'You have successfully marked a movie as watched!'
      : 'You have successfully marked a movie as not watched!';
    this.moviesService
      .updateWatchedState(this.movieDetails().id, state)
      .subscribe((_) => {
        this.movieDetails.mutate((movie) => (movie.watched = state));
        this.moviesService.refetchMovies();
        this.messageService.add({
          summary,
          detail,
        });
      });
  }

  onUpdateWatchlistState(state: boolean) {
    const summary = state ? 'Added on watchlist' : 'Removed from watchlist';
    const detail = state
      ? 'You have successfully added a movie to watchlist!'
      : 'You have successfully removed a movie from watchlist!';
    this.moviesService
      .updateWatchlistState(this.movieDetails().id, state)
      .subscribe((_) => {
        this.movieDetails.mutate((movie) => (movie.isOnWatchlist = state));
        this.moviesService.refetchMovies();
        this.messageService.add({
          summary,
          detail,
        });
      });
  }
}
