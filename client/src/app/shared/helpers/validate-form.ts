import { FormGroup } from '@angular/forms';

export function validateForm(fg: FormGroup) {
  if (!fg.valid) {
    Object.values(fg.controls).forEach((control) => {
      if (control.invalid) {
        control.markAsDirty();
        control.updateValueAndValidity({ onlySelf: true });
      }
    });
    return false;
  }
  return true;
}
