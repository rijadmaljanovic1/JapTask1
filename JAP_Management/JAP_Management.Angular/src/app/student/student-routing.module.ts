import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StudentHomeComponent } from './components/student-home/student-home.component';
import { StudentListComponent } from './components/student-list/student-list.component';

const routes: Routes = [
  {
    path:"",
    component:StudentHomeComponent,
    children:[
      {path:"", component:StudentListComponent},
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StudentRoutingModule { }
