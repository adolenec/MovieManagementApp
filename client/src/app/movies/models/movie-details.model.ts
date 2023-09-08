import { Dropdown } from 'src/app/shared/models/dropdown.model';

export interface MovieDetails {
  id: number;
  name: string;
  description: string;
  rating: number;
  duration: number;
  director: Dropdown;
  category: Dropdown;
  isFavourite: boolean;
  isOnWatchlist: boolean;
  watched: boolean;
}
