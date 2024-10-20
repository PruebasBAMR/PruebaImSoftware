import { afterRender, Component, Inject, OnInit } from '@angular/core';
import { PersonaService } from '../../service/persona.service';
import { AlertasService } from '../../service/alertas.service';
import { DOCUMENT } from '@angular/common';
import { stringify } from 'querystring';
import { IPersona_Results_Response } from '../../interfaces/IRegistrados.interface';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registrados-page',
  templateUrl: './registrados-page.component.html',
  styles: ``
})


export class RegistradosPageComponent implements OnInit {

  private tokenGen: string = '';

  public personas: IPersona_Results_Response[] = [];

  constructor(private router: Router, private service: PersonaService, private alertas: AlertasService, @Inject(DOCUMENT) private document: Document){

  }

  ngOnInit(): void {
    this.getPersonas();
  }

   private cargaTokenStorage():string {
    const localStorages = this.document.defaultView?.localStorage;

    if (localStorages) {
      return localStorage.getItem('token')!;
    }
    return ''
   }

  getPersonas() {

    this.service.getPersonas(this.cargaTokenStorage())
    .subscribe( personas => {
      if (!personas.ok) {
        return;
      }
      this.personas = personas.results
    },
    (err) => {
      // console.log(err)
      if (err.status == 401) {
        this.alertas.ventanaError('No autorizado');
        this.router.navigateByUrl('token');
      }
      this.alertas.ventanaError(err.error.results.message);
    });
  }

}
