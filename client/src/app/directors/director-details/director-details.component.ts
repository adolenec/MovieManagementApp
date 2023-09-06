import { Component, Input, inject } from '@angular/core';
import { DatePipe, NgIf } from '@angular/common';
import { DirectorsService } from '../directors.service';
import { toSignal } from '@angular/core/rxjs-interop';
import { DirectorDetails } from '../models/director-details.model';
import { CarouselModule } from 'primeng/carousel';
import { DividerModule } from 'primeng/divider';
import { MovieCardComponent } from 'src/app/movies/movie-card/movie-card.component';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import {
  DialogService,
  DynamicDialogModule,
  DynamicDialogRef,
} from 'primeng/dynamicdialog';
import { ConfirmModalComponent } from 'src/app/shared/confirm-modal/confirm-modal.component';
import { ModalConfg } from 'src/app/shared/models/modal-config.model';
import { DirectorFormComponent } from '../director-form/director-form.component';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-director-details',
  standalone: true,
  imports: [
    CarouselModule,
    DividerModule,
    DatePipe,
    MovieCardComponent,
    RouterLink,
    DynamicDialogModule,
    ConfirmModalComponent,
    NgIf,
  ],
  providers: [DialogService],
  templateUrl: './director-details.component.html',
  styleUrls: ['./director-details.component.scss'],
})
export class DirectorDetailsComponent {
  //route parameter
  @Input() id = '';

  private directorsService = inject(DirectorsService);
  private dialogService = inject(DialogService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private messageService = inject(MessageService);

  directorDetails$ = this.directorsService.directorDetails$;
  selectedDirectorId = this.directorsService.selectedDirectorId;

  directorDetails = toSignal(this.directorDetails$, {
    initialValue: {} as DirectorDetails,
  });

  private ref: DynamicDialogRef | undefined;

  ngOnInit() {
    if (this.id) this.selectedDirectorId.set(+this.id);
  }

  onConfirmDelete() {
    this.ref = this.dialogService.open(ConfirmModalComponent, {
      showHeader: false,
      width: '560px',
      data: {
        modalConfig: {
          header: 'Delete director?',
          message: 'Are you sure you want to delete this director?',
        } as ModalConfg,
      },
    });

    this.ref.onClose.subscribe((isConfirm: boolean) => {
      if (isConfirm) {
        this.directorsService.deleteDirector(+this.id).subscribe((_) => {
          this.router.navigate(['..'], { relativeTo: this.route });
          this.directorsService.refetchDirectors();
          this.messageService.add({
            summary: 'Director deleted',
            detail: 'You have successfully deleted a director!',
          });
        });
      }
    });
  }

  onUpdateDirectorClick() {
    this.ref = this.dialogService.open(DirectorFormComponent, {
      showHeader: false,
      width: '460px',
      data: {
        isAddMode: false,
        directorDetails: this.directorDetails(),
      },
    });

    this.ref.onClose.subscribe((directorDetails: DirectorDetails) => {
      if (directorDetails) {
        this.directorsService
          .updateDirector(directorDetails, +this.id)
          .subscribe((_) => {
            this.messageService.add({
              summary: 'Director updated',
              detail: 'You have successfully updated a director!',
            });
            this.router.navigate(['..'], { relativeTo: this.route });
            this.directorsService.refetchDirectors();
          });
      }
    });
  }
}
