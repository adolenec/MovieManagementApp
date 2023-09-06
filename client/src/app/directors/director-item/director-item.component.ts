import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { DatePipe } from '@angular/common';
import { RouterLink } from '@angular/router';
import { Director } from '../models/director.model';

@Component({
  selector: 'app-director-item',
  standalone: true,
  imports: [RouterLink, DatePipe],
  templateUrl: './director-item.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DirectorItemComponent {
  @Input({ required: true }) director!: Director;
}
