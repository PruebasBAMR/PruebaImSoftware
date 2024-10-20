import { Component, Inject } from '@angular/core';
import { AlertasService } from '../../service/alertas.service';
import { debug } from 'console';
import { INuevo_Request } from '../../interfaces/INuevo.interface';
import { PersonaService } from '../../service/persona.service';
import { DOCUMENT } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nueva-persona-page',
  templateUrl: './nueva-persona-page.component.html',
  styles: ``
})
export class NuevaPersonaPageComponent {
  txtNombre:string = '';
  txtEdad:string = '';
  txtEmail:string = '';

  private tokenGen: string = '';

  constructor(private router: Router, private service:PersonaService, private alertas:AlertasService, @Inject(DOCUMENT) private document: Document){}

  public registraPersona() {
    if (this.txtNombre == '' || this.txtEdad == '' || this.txtEmail == '') {
      this.alertas.ventanaError('Complete los campos')
    }
    // valida nombre
    if (!this.numCaracteres(this.txtNombre)) return this.alertas.ventanaError('El nombre no puede ser mayor a 50 caracteres')
    // validar email
    if (!this.esEmailValido(this.txtEmail)) return this.alertas.ventanaError('Email no valido')

      this.confirmaRegistro();
  }

  private numCaracteres(nombre:string):boolean {
    if (nombre.length > 50) {
      return false;
    }

    return true;
  }


  private esEmailValido(email: string):boolean {
    let mailValido = false;
      'use strict';

      var EMAIL_REGEX = /^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;

      if (email.match(EMAIL_REGEX)){
        mailValido = true;
      }
    return mailValido;
  }

  private cargaTokenStorage():string {
    const localStorages = this.document.defaultView?.localStorage;

    if (localStorages) {
      return this.tokenGen = localStorage.getItem('token')!;
    }
    return ''
   }

  public confirmaRegistro(){
    const persona : INuevo_Request = {
      nombre: this.txtNombre,
      edad: Number.parseInt(this.txtEdad),
      email: this.txtEmail,
    }

    this.service.registrarPersona(persona, this.cargaTokenStorage()).subscribe((data) => {
       if (data.ok == true) {
        this.txtNombre = ''
        this.txtEdad = ''
        this.txtEmail = ''
        return this.alertas.ventanaOk(data.results.message);
       }


    },
    (err) => {

      if (err.status == 401) {
        this.alertas.ventanaError('No autorizado');
        this.router.navigateByUrl('token');
      }
      //error
      this.alertas.ventanaError(err.error.results.message);
    });
   }
}
