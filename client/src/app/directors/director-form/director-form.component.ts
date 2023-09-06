import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  DialogService,
  DynamicDialogConfig,
  DynamicDialogRef,
} from 'primeng/dynamicdialog';
import { DirectorDetails } from '../models/director-details.model';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'app-director-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  providers: [DialogService],
  templateUrl: './director-form.component.html',
})
export class DirectorFormComponent {
  private dialogConfig = inject(DynamicDialogConfig);
  private ref = inject(DynamicDialogRef);
  isAddMode = this.dialogConfig.data.isAddMode as boolean;
  directorDetails = this.dialogConfig.data.directorDetails as DirectorDetails;

  directorForm = new FormGroup({
    firstName: new FormControl<string>(
      this.isAddMode ? '' : this.directorDetails.firstName,
      [Validators.required]
    ),
    lastName: new FormControl<string>(
      this.isAddMode ? '' : this.directorDetails.lastName,
      [Validators.required]
    ),
    description: new FormControl<string>(
      this.isAddMode ? '' : this.directorDetails.description,
      [Validators.required]
    ),
    dateOfBirth: new FormControl<string | null>(
      this.isAddMode
        ? null
        : new Date(this.directorDetails.dateOfBirth).toLocaleDateString(
            'en-ca'
          ),
      [Validators.required]
    ),
  });

  onCloseModal() {
    this.ref.close();
  }

  submitForm() {
    this.isAddMode ? this.createDirector() : this.updateDirector();
  }

  private validateForm() {
    if (!this.directorForm.valid) {
      Object.values(this.directorForm.controls).forEach((control) => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
      return false;
    }
    return true;
  }

  private createDirector() {
    if (!this.validateForm()) return;
    this.ref.close(this.directorForm.value);
  }

  private updateDirector() {
    if (!this.validateForm()) return;
    this.ref.close(this.directorForm.value);
  }
}
