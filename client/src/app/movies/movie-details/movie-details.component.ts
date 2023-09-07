import { Component, Input, inject } from '@angular/core';
import { MoviesService } from '../movies.service';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { MessageService } from 'primeng/api';
import { toSignal } from '@angular/core/rxjs-interop';
import { MovieDetails } from '../models/movie-details.model';
import { DividerModule } from 'primeng/divider';
import { MovieActionsComponent } from './movie-actions/movie-actions.component';

@Component({
  selector: 'app-movie-details',
  standalone: true,
  imports: [RouterLink, DividerModule, MovieActionsComponent],
  providers: [DialogService],
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.scss'],
})
export class MovieDetailsComponent {
  @Input() id = '';

  private moviesService = inject(MoviesService);
  private dialogService = inject(DialogService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private messageService = inject(MessageService);

  movieDetails$ = this.moviesService.movieDetails$;
  movieDetails = this.moviesService.movieDetails;
  selectedMovieId = this.moviesService.selectedMovieId;

  //readonly, used only to subscribe
  movieDetailsSub = toSignal(this.movieDetails$, {
    initialValue: {} as MovieDetails,
  });

  private ref: DynamicDialogRef | undefined;

  ngOnInit() {
    if (this.id) this.selectedMovieId.set(+this.id);
  }

  onUpdateMovieClick() {}

  onConfirmDelete() {}
}
