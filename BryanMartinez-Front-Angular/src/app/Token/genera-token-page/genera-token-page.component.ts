import { afterRender, Component } from '@angular/core';
import { PersonasModule } from '../../personas/personas.module';
import { Router } from '@angular/router';
import { IToken_Request } from '../../personas/interfaces/IToken.interface';
import { PersonaService } from '../../personas/service/persona.service';
import { AlertasService } from '../../personas/service/alertas.service';

@Component({
  selector: 'app-genera-token-page',
  templateUrl: './genera-token-page.component.html',
  styles: ``
})
export class GeneraTokenPageComponent {

  txtPin: string = '';


 constructor(private service: PersonaService, private alertas: AlertasService, private router: Router) {

 }



 public generaToken(){
  const pinJson : IToken_Request = {
    pin: this.txtPin,
  }

  this.service.generaToken(pinJson).subscribe((data) => {
     if (data.ok != true) {
      return this.alertas.ventanaError('Usuario o contraseña incorrectos');
     }
    localStorage.setItem('token', data.token.toString());

    this.router.navigateByUrl('personas');

  },
  (err) => {
    console.log(err)
    this.alertas.ventanaError(err.error.results.message);

  });
 }




}
