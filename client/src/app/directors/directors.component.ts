import { Component, computed, inject } from '@angular/core';
import { DatePipe } from '@angular/common';
import { DirectorsService } from './directors.service';
import { DataViewModule } from 'primeng/dataview';
import { InputTextModule } from 'primeng/inputtext';
import { RouterLink } from '@angular/router';
import { PaginatorModule } from 'primeng/paginator';
import { DirectorItemComponent } from './director-item/director-item.component';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { DirectorFormComponent } from './director-form/director-form.component';
import { DirectorDetails } from './models/director-details.model';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';

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
    DynamicDialogModule,
    ToastModule,
  ],
  providers: [DialogService],
  templateUrl: './directors.component.html',
  styleUrls: ['./directors.component.scss'],
})
export class DirectorsComponent {
  private directorsService = inject(DirectorsService);
  private dialogService = inject(DialogService);
  private messageService = inject(MessageService);

  directors = this.directorsService.directors;
  directorsRequest = this.directorsService.directorsRequest;
  loadingDirectors = this.directorsService.loadingDirectors;

  ref: DynamicDialogRef | undefined;
  // ref = inject(DynamicDialogRef);

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

  onAddDirectorClick() {
    this.ref = this.dialogService.open(DirectorFormComponent, {
      showHeader: false,
      width: '460px',
      data: {
        isAddMode: true,
      },
    });

    this.ref.onClose.subscribe((directorDetails: DirectorDetails) => {
      if (directorDetails) {
        this.directorsService.createDirector(directorDetails).subscribe((_) => {
          this.messageService.add({
            summary: 'Director created',
            detail: 'You have successfully created a director!',
          });
          this.directorsService.refetchDirectors();
        });
      }
    });
  }
}
