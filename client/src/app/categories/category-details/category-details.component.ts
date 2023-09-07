import { Component, Input, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoriesService } from '../categories.service';
import { toSignal } from '@angular/core/rxjs-interop';
import { CategoryDetails } from '../models/category-details.model';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { DividerModule } from 'primeng/divider';
import { CarouselModule } from 'primeng/carousel';
import { MovieCardComponent } from 'src/app/movies/movie-card/movie-card.component';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ConfirmModalComponent } from 'src/app/shared/confirm-modal/confirm-modal.component';
import { ModalConfg } from 'src/app/shared/models/modal-config.model';
import { MessageService } from 'primeng/api';
import { CategoryFormComponent } from '../category-form/category-form.component';

@Component({
  selector: 'app-category-details',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    DividerModule,
    CarouselModule,
    MovieCardComponent,
  ],
  providers: [DialogService],
  templateUrl: './category-details.component.html',
  styleUrls: ['./category-details.component.scss'],
})
export class CategoryDetailsComponent implements OnInit {
  //route parameter
  @Input() id = '';

  private categoriesService = inject(CategoriesService);
  private dialogService = inject(DialogService);
  private messageService = inject(MessageService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  categoryDetails$ = this.categoriesService.categoryDetails$;
  selectedCategoryId = this.categoriesService.selectedCategoryId;

  categoryDetails = toSignal(this.categoryDetails$, {
    initialValue: {} as CategoryDetails,
  });

  private ref: DynamicDialogRef | undefined;

  ngOnInit() {
    if (this.id) this.selectedCategoryId.set(+this.id);
  }

  onConfirmDelete() {
    this.ref = this.dialogService.open(ConfirmModalComponent, {
      showHeader: false,
      width: '560px',
      data: {
        modalConfig: {
          header: 'Delete category?',
          message: 'Are you sure you want to delete this category?',
        } as ModalConfg,
      },
    });

    this.ref.onClose.subscribe((isConfirm: boolean) => {
      if (isConfirm) {
        this.categoriesService.deleteCategory(+this.id).subscribe((_) => {
          this.router.navigate(['..'], { relativeTo: this.route });
          this.categoriesService.refetchCategories();
          this.messageService.add({
            summary: 'Category deleted',
            detail: 'You have successfully deleted a category!',
          });
        });
      }
    });
  }

  onUpdateCategoryClick() {
    this.ref = this.dialogService.open(CategoryFormComponent, {
      showHeader: false,
      width: '460px',
      data: {
        isAddMode: false,
        categoryDetails: this.categoryDetails(),
      },
    });

    this.ref.onClose.subscribe((categoryDetails: CategoryDetails) => {
      if (categoryDetails) {
        this.categoriesService
          .updateCategory(categoryDetails, +this.id)
          .subscribe((_) => {
            this.messageService.add({
              summary: 'Category updated',
              detail: 'You have successfully updated a category!',
            });
            this.router.navigate(['..'], { relativeTo: this.route });
            this.categoriesService.refetchCategories();
          });
      }
    });
  }
}
