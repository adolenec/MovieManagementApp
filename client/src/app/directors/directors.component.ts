import { Component, computed, inject } from '@angular/core';
import { DatePipe } from '@angular/common';
import { DirectorsService } from './directors.service';
import { DataViewModule } from 'primeng/dataview';
import { InputTextModule } from 'primeng/inputtext';
import { RouterLink } from '@angular/router';
import { PaginatorModule } from 'primeng/paginator';
import { DirectorItemComponent } from './director-item/director-item.component';

@Component({
  selector: 'app-directors',
  standalone: true,
  imports: [
    DataViewModule,
    DatePipe,
    InputTextModule,
    RouterLink,
    PaginatorModule,
    DirectorItemComponent,
  ],
  templateUrl: './directors.component.html',
  styleUrls: ['./directors.component.scss'],
})
export class DirectorsComponent {
  private directorsService = inject(DirectorsService);
  directors = this.directorsService.directors;
  directorsRequest = this.directorsService.directorsRequest;
  loadingDirectors = this.directorsService.loadingDirectors;

  firstItemIndex = computed(
    () => (this.directorsRequest().page - 1) * this.directorsRequest().pageSize
  );

  onSearchDirectors(searchTerm: string) {
    this.directorsRequest.mutate((req) => (req.searchTerm = searchTerm));
  }

  onPageChange({ first, rows }: any) {
    const page = first / rows + 1;

    this.directorsRequest.set({
      ...this.directorsRequest(),
      page,
      pageSize: rows,
    });
  }
}
