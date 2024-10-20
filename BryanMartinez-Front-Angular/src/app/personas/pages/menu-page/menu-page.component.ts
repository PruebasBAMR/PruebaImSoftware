import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-menu-page',
  templateUrl: './menu-page.component.html',
  styles: ``
})
export class MenuPageComponent {
  constructor(private router:Router){
    this.router.navigateByUrl('personas/home');
  }
}
