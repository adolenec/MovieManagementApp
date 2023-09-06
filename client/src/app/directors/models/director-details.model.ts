import { Movie } from "src/app/movies/models/movie.model";

export interface DirectorDetails {
  id: number;
  firstName: string;
  lastName: string;
  description: string;
  dateOfBirth: Date;
  movies: Movie[]
}
