import { Component, inject } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ModalConfg } from '../models/modal-config.model';

@Component({
  selector: 'app-confirm-modal',
  standalone: true,
  template: `
    <div class="text-slate-300">
      <h1 class="text-2xl p-4 bg-slate-600">
        {{ modalConfig.header }}
      </h1>
      <div class="py-8 px-4">{{ modalConfig.message }}</div>
      <div class="flex justify-end gap-4 items-center p-6">
        <button (click)="onModalClose(false)" class="button">Close</button>
        <button (click)="onModalClose(true)" class="button">Confirm</button>
      </div>
    </div>
  `,
  styles: [
    `
      .button {
        @apply px-8 py-2 rounded-lg hover:bg-slate-400 hover:text-slate-200;
      }
    `,
  ],
})
export class ConfirmModalComponent {
  private dialogConfig = inject(DynamicDialogConfig);
  private ref = inject(DynamicDialogRef);
  modalConfig = this.dialogConfig.data.modalConfig as ModalConfg;

  onModalClose(isConfirm: boolean) {
    this.ref.close(isConfirm);
  }
}
