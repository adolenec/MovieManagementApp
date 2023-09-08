import { Component, Input, inject } from '@angular/core';
import { MoviesService } from '../movies.service';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { MessageService } from 'primeng/api';
import { toSignal } from '@angular/core/rxjs-interop';
import { MovieDetails } from '../models/movie-details.model';
import { DividerModule } from 'primeng/divider';
import { MovieActionsComponent } from './movie-actions/movie-actions.component';
import { ConfirmModalComponent } from 'src/app/shared/confirm-modal/confirm-modal.component';
import { ModalConfg } from 'src/app/shared/models/modal-config.model';
import { NgIf } from '@angular/common';
import { MovieFormComponent } from '../movie-form/movie-form.component';
import { CreateMovie } from '../models/create-movie.model';

@Component({
  selector: 'app-movie-details',
  standalone: true,
  imports: [RouterLink, DividerModule, MovieActionsComponent, NgIf],
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
  detailsLoading = this.moviesService.detailsLoading;

  //readonly, used only to subscribe
  movieDetailsSub = toSignal(this.movieDetails$, {
    initialValue: {} as MovieDetails,
  });

  private ref: DynamicDialogRef | undefined;

  ngOnInit() {
    if (this.id) this.selectedMovieId.set(+this.id);
  }

  onUpdateMovieClick() {
    this.ref = this.dialogService.open(MovieFormComponent, {
      showHeader: false,
      width: '460px',
      data: {
        isAddMode: false,
        movieDetails: this.movieDetails(),
      },
    });

    this.ref.onClose.subscribe((movie: CreateMovie) => {
      if (movie) {
        this.moviesService
          .updateMovie(movie, +this.id)
          .subscribe((_) => {
            this.messageService.add({
              summary: 'Movie updated',
              detail: 'You have successfully updated a movie!',
            });
            this.router.navigate(['..'], { relativeTo: this.route });
            this.moviesService.refetchMovies();
          });
      }
    });
  }

  onConfirmDelete() {
    this.ref = this.dialogService.open(ConfirmModalComponent, {
      showHeader: false,
      width: '560px',
      data: {
        modalConfig: {
          header: 'Delete movie?',
          message: 'Are you sure you want to delete this movie?',
        } as ModalConfg,
      },
    });

    this.ref.onClose.subscribe((isConfirm: boolean) => {
      if (isConfirm) {
        this.moviesService.deleteMovie(+this.id).subscribe((_) => {
          this.router.navigate(['..'], { relativeTo: this.route });
          this.moviesService.refetchMovies();
          this.messageService.add({
            summary: 'Movie deleted',
            detail: 'You have successfully deleted a movie!',
          });
        });
      }
    });
  }
}
