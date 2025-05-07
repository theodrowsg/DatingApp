import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideAnimations } from '@angular/platform-browser/animations';

import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideToastr } from 'ngx-toastr';
import { errorInterceptor } from './_interceptors/error.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(withInterceptors([errorInterceptor])),
    provideAnimations(),
    provideToastr({
      timeOut: 3000,
      progressBar: true,
      progressAnimation: 'decreasing',
      closeButton: true,
      preventDuplicates: true,
      positionClass: 'toast-bottom-right',
      enableHtml: true,
      tapToDismiss: true,
      maxOpened: 5,
      autoDismiss: false
    })
  ]
};
