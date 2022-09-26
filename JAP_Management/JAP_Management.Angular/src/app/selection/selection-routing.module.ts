import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SelectionHomeComponent } from './components/selection-home/selection-home.component';
import { SelectionListComponent } from './components/selection-list/selection-list.component';

const routes: Routes = [
  {
    path:"",
    component:SelectionHomeComponent,
    children:[
      {path:"", component:SelectionListComponent},
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SelectionRoutingModule { }
