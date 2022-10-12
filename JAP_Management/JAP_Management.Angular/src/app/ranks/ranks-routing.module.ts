import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RanksHomeComponent } from './components/ranks-home/ranks-home.component';
import { RanksListComponent } from './components/ranks-list/ranks-list.component';

const routes: Routes = [
  {
    path:"",
    component:RanksHomeComponent,
    children:[
      {path:"", component:RanksListComponent},
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RanksRoutingModule { }
