import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { BreadcrumbModule } from 'primeng/breadcrumb';

@Component({
  selector: 'app-movie-layout',
  standalone: true,
  imports: [RouterLink, RouterOutlet, BreadcrumbModule],
  templateUrl: './movie-layout.component.html',
  styleUrls: ['./movie-layout.component.scss'],
})
export class MovieLayoutComponent implements OnInit {
  items: MenuItem[] | undefined;
  ngOnInit() {
    this.items = [
      { label: 'All movies', routerLink: ['all'] },
      { label: 'Watchlist', routerLink: ['watchlist'] },
      {
        label: 'Favourites',
        routerLink: ['favourites'],
      },
    ];
  }
}
