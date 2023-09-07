import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { Movie } from '../models/movie.model';
import { RouterLink } from '@angular/router';
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-movie-item',
  standalone: true,
  imports: [RouterLink, NgClass],
  templateUrl: './movie-item.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MovieItemComponent {
  @Input({ required: true }) movie!: Movie;
}
