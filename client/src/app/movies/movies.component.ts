import { Component, computed, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterOutlet } from '@angular/router';
import { MoviesService } from './movies.service';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { MessageService } from 'primeng/api';
import { FormsModule } from '@angular/forms';
import { DataViewModule } from 'primeng/dataview';
import { MovieItemComponent } from './movie-item/movie-item.component';

@Component({
  selector: 'app-movies',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    RouterLink,
    FormsModule,
    DataViewModule,
    MovieItemComponent,
  ],
  providers: [DialogService],
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.scss'],
})
export class MoviesComponent {
  private moviesService = inject(MoviesService);
  private dialogService = inject(DialogService);
  private messageService = inject(MessageService);

  movies = this.moviesService.movies;
  moviesRequest = this.moviesService.moviesRequest;
  loadingMovies = this.moviesService.loadingMovies;

  ref: DynamicDialogRef | undefined;

  firstItemIndex = computed(
    () => (this.moviesRequest().page - 1) * this.moviesRequest().pageSize
  );

  onSearchMovies(searchTerm: string) {
    this.moviesRequest.mutate((req) => (req.searchTerm = searchTerm));
  }

  onPageChange({ first, rows }: any) {
    const page = first / rows + 1;

    this.moviesRequest.set({
      ...this.moviesRequest(),
      page,
      pageSize: rows,
    });
  }

  onAddDirectorClick() {}
}
