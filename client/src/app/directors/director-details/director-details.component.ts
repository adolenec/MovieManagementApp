import { Component, Input, inject } from '@angular/core';
import { DatePipe } from '@angular/common';
import { DirectorsService } from '../directors.service';
import { toSignal } from '@angular/core/rxjs-interop';
import { DirectorDetails } from '../models/director-details.model';
import { CarouselModule } from 'primeng/carousel';
import { DividerModule } from 'primeng/divider';
import { MovieCardComponent } from 'src/app/movies/movie-card/movie-card.component';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-director-details',
  standalone: true,
  imports: [
    CarouselModule,
    DividerModule,
    DatePipe,
    MovieCardComponent,
    RouterLink,
  ],
  templateUrl: './director-details.component.html',
  styleUrls: ['./director-details.component.scss'],
})
export class DirectorDetailsComponent {
  //route parameter
  @Input() id = '';

  private directorsService = inject(DirectorsService);

  directorDetails$ = this.directorsService.directorDetails$;
  selectedDirectorId = this.directorsService.selectedDirectorId;

  directorDetails = toSignal(this.directorDetails$, {
    initialValue: {} as DirectorDetails,
  });

  ngOnInit() {
    if (this.id) this.selectedDirectorId.set(+this.id);
  }
}
