import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { ImageUploadModule } from './app/app.image-upload-with-preview.module';
import { environment } from './environments/environment';


if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic().bootstrapModule(ImageUploadModule)
  .catch(err => console.error(err));  
