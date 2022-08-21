import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgxGraphModule } from '@swimlane/ngx-graph';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatIconModule} from '@angular/material/icon';
import { HttpClientModule } from '@angular/common/http';
import {MatListModule} from '@angular/material/list';
import { FormsModule } from '@angular/forms';
import {MaterialExampleModule} from '../materials.module';
import { SelectAdventureComponent } from './select.adventure/select.adventure.component';
import { DialogTemplateComponent, DialogOverviewExampleDialog } from './dialog.template/dialog.template.component';


@NgModule({
  declarations: [
    AppComponent,
    SelectAdventureComponent,
    DialogTemplateComponent,
    DialogOverviewExampleDialog
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgxGraphModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatIconModule,
    HttpClientModule,
    MatListModule,
    FormsModule,
    MaterialExampleModule
  ],
  entryComponents: [DialogTemplateComponent, DialogOverviewExampleDialog],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
