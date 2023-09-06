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

  directorDetails$ = this.directorsService.directorDetails$;
  selectedDirectorId = this.directorsService.selectedDirectorId;

  directorDetails = toSignal(this.directorDetails$, {
    initialValue: {} as DirectorDetails,
  });

  ref: DynamicDialogRef | undefined;

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
        });
      }
    });
  }
}
