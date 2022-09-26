import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuardService } from './core/guards/auth-guard.service';
import { LoginRouteGuardService } from './core/guards/login-route-guard.service';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
  {
    path: 'student',
    loadChildren: () => import('./student/student.module').then(mod => mod.StudentModule),
    canActivate:[AuthGuardService]
  },
  {
    path: 'program',
    loadChildren: () => import('./program/program.module').then(mod => mod.ProgramModule),
    canActivate:[AuthGuardService]
  },
  {
    path: 'selection',
    loadChildren: () => import('./selection/selection.module').then(mod => mod.SelectionModule),
    canActivate:[AuthGuardService]
  },
  {
    path:'login',
    component:LoginComponent,
    canActivate:[LoginRouteGuardService]
  },
{
  path:'**',
  redirectTo:'/student',
  pathMatch:'full'
},
  {
    path:'',
    redirectTo:'/student',
    pathMatch:'full'
  },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
  })
  export class AppRoutingModule { }