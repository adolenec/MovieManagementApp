import { Dropdown } from "primeng/dropdown";

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
