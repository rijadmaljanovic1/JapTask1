import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminGuard } from './core/guards/admin.guard';
import { AuthGuardService } from './core/guards/auth-guard.service';
import { LoginRouteGuardService } from './core/guards/login-route-guard.service';
import { LoginComponent } from './login/login.component';
import { UserProfileComponent } from './user-profile/user-profile.component';

const routes: Routes = [
  {
    path: 'student',
    loadChildren: () => import('./student/student.module').then(mod => mod.StudentModule),
    canActivate:[AdminGuard]
  },
  {
    path: 'program',
    loadChildren: () => import('./program/program.module').then(mod => mod.ProgramModule),
    canActivate:[AdminGuard]
  },
  {
    path: 'selection',
    loadChildren: () => import('./selection/selection.module').then(mod => mod.SelectionModule),
    canActivate:[AdminGuard]
  },
  {
    path: 'ranks',
    loadChildren: () => import('./ranks/ranks.module').then(mod => mod.RanksModule),
    canActivate:[AdminGuard]
  },
  {
    path:'login',
    component:LoginComponent,
    canActivate:[LoginRouteGuardService]
  },
  {
    path:'user-profile',
    component:UserProfileComponent,
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