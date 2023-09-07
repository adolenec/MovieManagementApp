import { Movie } from 'src/app/movies/models/movie.model';

export interface CategoryDetails {
  id: number;
  name: string;
  description: string;
  movies: Movie[];
}
