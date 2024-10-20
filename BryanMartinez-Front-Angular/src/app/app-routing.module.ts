import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GeneraTokenPageComponent } from './Token/genera-token-page/genera-token-page.component';

const routes: Routes = [
  {
    path: 'token',
    component: GeneraTokenPageComponent
  },
  {
    path: 'personas',
    loadChildren: () => import('./personas/personas.module').then( m => m.PersonasModule )
  },
  {
    path: '**',
    redirectTo: 'token'
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
