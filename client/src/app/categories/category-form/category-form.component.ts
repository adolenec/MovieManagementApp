import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { CategoryDetails } from '../models/category-details.model';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { validateForm } from 'src/app/shared/helpers/validate-form';

@Component({
  selector: 'app-category-form',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './category-form.component.html',
  styleUrls: ['./category-form.component.scss'],
})
export class CategoryFormComponent {
  private dialogConfig = inject(DynamicDialogConfig);
  private ref = inject(DynamicDialogRef);
  isAddMode = this.dialogConfig.data.isAddMode as boolean;
  categoryDetails = this.dialogConfig.data.categoryDetails as CategoryDetails;

  categoryForm = new FormGroup({
    name: new FormControl<string>(
      this.isAddMode ? '' : this.categoryDetails.name,
      [Validators.required]
    ),
    description: new FormControl<string>(
      this.isAddMode ? '' : this.categoryDetails.description,
      [Validators.required]
    ),
  });

  onCloseModal() {
    this.ref.close();
  }

  submitForm() {
    this.isAddMode ? this.createCategory() : this.updateCategory();
  }

  private createCategory() {
    if (!validateForm(this.categoryForm)) return;
    this.ref.close(this.categoryForm.value);
  }

  private updateCategory() {
    if (!validateForm(this.categoryForm)) return;
    this.ref.close(this.categoryForm.value);
  }
}
