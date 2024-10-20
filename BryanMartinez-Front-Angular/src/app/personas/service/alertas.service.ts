import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';

@Injectable({providedIn: 'root'})
export class AlertasService {
  constructor() { }


  public ventanaError(title: string){
    Swal.fire({
      icon: "error",
      title: title,
      showConfirmButton: false,
      timer: 3000
    });
  }

  public ventanaOk(title:string){
    Swal.fire({
      title,
      icon: "success",
      showConfirmButton: false,
      timer: 2000
    });
  }
}
