import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProgramItemsHomeComponent } from './components/program-items-home/program-items-home.component';
import { ProgramItemsListComponent } from './components/program-items-list/program-items-list.component';

const routes: Routes = [
  {
    path:"",
    component:ProgramItemsHomeComponent,
    children:[
      {path:"", component:ProgramItemsListComponent},
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProgramItemsRoutingModule { }
