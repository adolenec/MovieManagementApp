import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Category } from '../models/category.model';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-category-item',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './category-item.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CategoryItemComponent {
  @Input({ required: true }) category!: Category;
}
