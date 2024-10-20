import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
// import { MenuPageComponent } from './Pages/menu-page/menu-page.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { SharedModule } from '../shared/shared.module';
import { PersonasRoutingModule } from './personas-routing.module';
import { NuevaPersonaPageComponent } from './pages/nueva-persona-page/nueva-persona-page.component';
import { RegistradosPageComponent } from './pages/registrados-page/registrados-page.component';
import { MenuPageComponent } from './pages/menu-page/menu-page.component';
import { MdbFormsModule } from 'mdb-angular-ui-kit/forms';



@NgModule({
  declarations: [
    HomePageComponent,
    NuevaPersonaPageComponent,
    RegistradosPageComponent,
    MenuPageComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    PersonasRoutingModule,
    // FormsModule,
    MdbFormsModule,
    FormsModule,
  ],

})
export class PersonasModule { }
