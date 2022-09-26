import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProgramHomeComponent } from './components/program-home/program-home.component';
import { ProgramListComponent } from './components/program-list/program-list.component';

const routes: Routes = [
  {
    path:"",
    component:ProgramHomeComponent,
    children:[
      {path:"", component:ProgramListComponent},
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProgramRoutingModule { }
