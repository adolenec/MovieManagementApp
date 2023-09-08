import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  DialogService,
  DynamicDialogConfig,
  DynamicDialogRef,
} from 'primeng/dynamicdialog';
import { MovieDetails } from '../models/movie-details.model';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { DropdownService } from 'src/app/shared/services/dropdown.service';
import { DropdownModule } from 'primeng/dropdown';
import { validateForm } from 'src/app/shared/helpers/validate-form';

@Component({
  selector: 'app-movie-form',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, DropdownModule],
  providers: [DialogService],
  templateUrl: './movie-form.component.html',
  styleUrls: ['./movie-form.component.scss'],
})
export class MovieFormComponent {
  private dialogConfig = inject(DynamicDialogConfig);
  private ref = inject(DynamicDialogRef);
  private dropdownService = inject(DropdownService);

  categoriesDropdown = this.dropdownService.categoriesDropdown;
  directorsDropdown = this.dropdownService.directorsDropdown;

  directorsDropdownRequest = this.dropdownService.directorsDropdownRequest;
  categoriesDropdownRequest = this.dropdownService.categoryDropdownRequest;

  isAddMode = this.dialogConfig.data.isAddMode as boolean;
  movieDetails = this.dialogConfig.data.movieDetails as MovieDetails;

  movieForm = new FormGroup({
    name: new FormControl<string>(
      this.isAddMode ? '' : this.movieDetails.name,
      [Validators.required]
    ),
    description: new FormControl<string>(
      this.isAddMode ? '' : this.movieDetails.description,
      [Validators.required]
    ),
    rating: new FormControl<number | null>(
      this.isAddMode ? null : this.movieDetails.rating,
      [Validators.required, Validators.min(1), Validators.max(10)]
    ),
    duration: new FormControl<number | null>(
      this.isAddMode ? null : this.movieDetails.duration,
      [Validators.required, Validators.min(1)]
    ),
    directorId: new FormControl<number | null>(
      this.isAddMode ? null : this.movieDetails.director.id,
      [Validators.required]
    ),
    categoryId: new FormControl<number | null>(
      this.isAddMode ? null : this.movieDetails.category.id,
      [Validators.required]
    ),
  });

  onSearchDropdownDirectors(searchTerm: string) {
    this.directorsDropdownRequest.set(searchTerm);
  }

  onSearchDropdownCategories(searchTerm: string) {
    this.categoriesDropdownRequest.set(searchTerm);
  }

  submitForm() {
    this.isAddMode ? this.createDirector() : this.updateDirector();
  }

  onCloseModal() {
    this.ref.close();
  }

  private createDirector() {
    if (!validateForm(this.movieForm)) return;
    this.ref.close(this.movieForm.value);
  }

  private updateDirector() {
    if (!validateForm(this.movieForm)) return;
    this.ref.close(this.movieForm.value);
  }
}
