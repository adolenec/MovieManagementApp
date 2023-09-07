import { Component, computed, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoriesService } from './categories.service';
import { PaginatorModule } from 'primeng/paginator';
import { DataViewModule } from 'primeng/dataview';
import { CategoryItemComponent } from './category-item/category-item.component';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { CategoryFormComponent } from './category-form/category-form.component';
import { CategoryDetails } from './models/category-details.model';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-categories',
  standalone: true,
  imports: [
    CommonModule,
    PaginatorModule,
    DataViewModule,
    CategoryItemComponent,
  ],
  providers: [DialogService],
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.scss'],
})
export class CategoriesComponent {
  private categoriesService = inject(CategoriesService);
  private dialogService = inject(DialogService);
  private messageService = inject(MessageService);

  categories = this.categoriesService.categories;
  categoriesRequest = this.categoriesService.categoriesRequest;
  loadingCategories = this.categoriesService.loadingCategories;

  firstItemIndex = computed(
    () =>
      (this.categoriesRequest().page - 1) * this.categoriesRequest().pageSize
  );

  ref: DynamicDialogRef | undefined;

  onPageChange({ first, rows }: any) {
    const page = first / rows + 1;

    this.categoriesRequest.set({
      ...this.categoriesRequest(),
      page,
      pageSize: rows,
    });
  }

  onSearchCategories(searchTerm: string) {
    this.categoriesRequest.mutate((req) => (req.searchTerm = searchTerm));
  }

  onAddCategoryClick() {
    this.ref = this.dialogService.open(CategoryFormComponent, {
      showHeader: false,
      width: '460px',
      data: {
        isAddMode: true,
      },
    });

    this.ref.onClose.subscribe((categoryDetails: CategoryDetails) => {
      if (categoryDetails) {
        this.categoriesService
          .createCategory(categoryDetails)
          .subscribe((_) => {
            this.messageService.add({
              summary: 'Category created',
              detail: 'You have successfully created a category!',
            });
            this.categoriesService.refetchCategories();
          });
      }
    });
  }
}
