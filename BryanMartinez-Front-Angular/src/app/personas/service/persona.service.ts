import { HttpClient, HttpHeaders } from '@angular/common/http';
import { afterRender, Injectable } from '@angular/core';
// import { ILogin, ILogin_Response, IPeliculas_Response } from '../../interfaces/login.interface';
import { Observable, tap } from 'rxjs';
import { IToken_Request, IToken_Response } from '../interfaces/IToken.interface';
import { IPersona_Response } from '../interfaces/IRegistrados.interface';
import { INuevo_Request, INuevo_Response } from '../interfaces/INuevo.interface';


@Injectable({providedIn: 'root'})
export class PersonaService {

   private apiUrl: string = 'https://localhost:7255/api';
  //  private apiUrl: string = 'http://192.168.1.103:8011/api';


  constructor( private http: HttpClient ) {}




  public generaToken( pinJson: IToken_Request): Observable<IToken_Response> {
    const url = `${ this.apiUrl }/Token/GeneraToken`;
    return this.http.post<IToken_Response>( url, pinJson );
  }


  public getPersonas(token:string): Observable<IPersona_Response> {
    let headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    const url = `${ this.apiUrl }/Persona/GetPersonas`;
    return this.http.get<IPersona_Response>( url, {headers}  )

  }


  public registrarPersona( persona: INuevo_Request, token:string): Observable<INuevo_Response> {
    let headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    const url = `${ this.apiUrl }/Persona/NuevaPersona`;
    return this.http.post<INuevo_Response>( url, persona, {headers} );
  }


}
