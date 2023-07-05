import { Component, OnInit } from '@angular/core';
import { Post } from '../../services/Post'
import { PostService } from 'src/services/PostService';
import { UserService } from '../../services/UserService'
import { User } from 'src/services/User';
import { AthenticateService } from 'src/services/AthenticateService';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css'],
})

export class MainComponent 
  implements OnInit{

    posts: Post[] = [];
    pageNumber: number = 0;
    userId: string | null = sessionStorage.getItem('userId');
    user: User = 
    {
      bornDate: new Date,
      id: 0,
      completeName: '',
      username: '',
      photo: '',
      mail: '',
      isAuth: 0
    };

    filters: string = '';

    
    constructor(
      private service: PostService,
      private userService: UserService,
      private authenticator: AthenticateService) { }

    ngOnInit(): void {
      console.log("enviando: ");
      console.log(sessionStorage.getItem('jwtAuthenticator'));
      
      this.authenticator.authenticate(
        sessionStorage.getItem('jwtAuthenticator')
      )


      this
        .userService
        .getUser(
          sessionStorage.getItem("userId")
        ).subscribe(
        x => this.user = x
      )

      //this.filters = this.getFiltersString();
      
      this.update(this.filters, this.pageNumber);
    }

    update(filters: string, page: number)
    {
      this.service.getPage(filters, page)
        .subscribe(x => 
          x.forEach(e => this.posts.push(e))
        )
      console.log(this.posts)
      
      this.pageNumber += 1;
    }

    getFiltersString()
    {
      
    }
}
