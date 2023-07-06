import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AthenticateService } from 'src/services/AthenticateService';
import { Forum } from 'src/services/Forum';
import { ForumService } from 'src/services/ForumService';
import { User } from 'src/services/User';
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

  forums : Forum[] = []
  notFound : boolean = false;

  ngOnInit(): void {
    this.loginService.authenticate(sessionStorage.getItem('jwtAuthenticator'))

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

    // this.forumService.getForumByName().subscribe(
    //   forumList => this.forums = forumList
    // )
  }


}
