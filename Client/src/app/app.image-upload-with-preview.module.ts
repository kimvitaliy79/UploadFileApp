import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { ImageUploadWithPreviewComponent } from './app.image-upload-with-preview.component';
import { ListFilesComponent } from './app.list-files.component';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    ImageUploadWithPreviewComponent,
    ListFilesComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [ ListFilesComponent]
})
export class ImageUploadModule {

}
