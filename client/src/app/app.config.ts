import { ApplicationConfig, InjectionToken } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideAnimations } from '@angular/platform-browser/animations';
import { APP_ROUTES } from './app.routes';
import { provideHttpClient } from '@angular/common/http';

export const API_URL = new InjectionToken<string>('apiUrl');

export const appConfig: ApplicationConfig = {
  providers: [
    { provide: API_URL, useValue: 'https://localhost:4000' },
    provideRouter(APP_ROUTES),
    provideHttpClient(),
    provideAnimations(),
  ],
};
