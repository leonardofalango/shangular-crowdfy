import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-search-box',
  templateUrl: './search-box.component.html',
  styleUrls: ['./search-box.component.css']
})
export class SearchBoxComponent {
  constructor (
    private router: Router
  ) { }

  searchText: string = ''

  search()
  {
    this.router.navigate(['/search/' + this.searchText])
  }
}
