import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AthenticateService } from 'src/services/AthenticateService';
import { Forum } from 'src/services/Forum';
import { ForumService } from 'src/services/ForumService';
import { UserService } from 'src/services/UserService';

@Component({
  selector: 'app-search-page',
  templateUrl: './search-page.component.html',
  styleUrls: ['./search-page.component.css']
})
export class SearchPageComponent
  implements OnInit {
  constructor(
    private userService: UserService,
    private loginService: AthenticateService,
    private forumService: ForumService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  forumDosLindoes : Forum = {
    id: 1,
    title: "Forum dos lindoes",
    description: "Forum dos lindoes",
    createdAt: new Date(),
    photo: '',
    selected: true
  }

  notFollowingForums : Forum = {
    id: 2,
    title: "Not following forums",
    description: "Not following forums",
    createdAt: new Date(),
    photo: '',
    selected: false
  }

  forums : Forum[] = [
    this.forumDosLindoes,
    this.notFollowingForums,
    this.forumDosLindoes,
    this.notFollowingForums
  ]
  notFound : boolean = false;
  searchText : string = ""

  ngOnInit(): void {
    this.loginService.authenticate(sessionStorage.getItem('jwtAuthenticator'))


    // get the url params then search by forum name
    this.route.queryParams.subscribe(
      params => {
        console.log(params)
        this.forumService.getForumByName(
          params['searchText']
        ).subscribe({
          next: (res: Forum[]) => {
            this.forums = res;
          },
          error: (err: HttpErrorResponse) => {
            this.notFound = true;
            this.forumService.getAllForums().subscribe(
              res => this.forums = res
            )
          }
        })
      }

    )

  }
  
  search = () => {
    this.router.navigate(['/search'], {queryParams: {searchText: this.searchText}})
  }

}
