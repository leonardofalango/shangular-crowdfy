import { Component, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/services/UserService';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})

@Injectable({
  providedIn: 'root'
})

export class HeaderComponent {
  
  constructor (
    private service: UserService,
    private router: Router
    ) { }

  logout()
  {
    this.service.logout()
    this.router.navigate(['login'])
  }
}
