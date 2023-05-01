import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './core/authentication/login/login.component';


const routes: Routes = [
  { path:'login',pathMatch:'full',component: LoginComponent},
  { path: '', pathMatch: 'full', redirectTo: '/cars' },
  { path: '**', pathMatch: 'full', redirectTo: '/cars'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
