import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SelectAdventureComponent } from './select.adventure/select.adventure.component';
import { DialogTemplateComponent} from './dialog.template/dialog.template.component'

const routes: Routes = [
  { path: '', component: SelectAdventureComponent },
  { path: 'game/:id', component: DialogTemplateComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
