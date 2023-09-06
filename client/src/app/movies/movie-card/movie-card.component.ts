import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { Movie } from '../models/movie.model';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-movie-card',
  standalone: true,
  templateUrl: './movie-card.component.html',
  imports: [RouterLink],
  // changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MovieCardComponent {
  @Input({ required: true }) movie!: Movie;
}
