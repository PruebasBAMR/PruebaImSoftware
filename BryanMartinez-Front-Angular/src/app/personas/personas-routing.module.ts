

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { NuevaPersonaPageComponent } from './pages/nueva-persona-page/nueva-persona-page.component';
import { RegistradosPageComponent } from './pages/registrados-page/registrados-page.component';
import { MenuPageComponent } from './pages/menu-page/menu-page.component';

const routes: Routes = [
  {
    path: '',
    component: MenuPageComponent,
    children: [
      {
        path: 'home',
        component: HomePageComponent,
      },
      {
        path: 'nuevo',
        component: NuevaPersonaPageComponent,
      },
      {
        path: 'registrados',
        component: RegistradosPageComponent,
      },
    ]
  },
  {
    path: '**',
    redirectTo: 'personas/home'
  },

];

@NgModule({
  imports: [
    RouterModule.forChild( routes )
  ],
  exports: [
    RouterModule
  ],
})

export class PersonasRoutingModule {}
