import { Component, ElementRef, OnInit } from '@angular/core';
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

    posts: Post[] = [
    ];

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

      this.filters = this.getFiltersString();
      
      this.update();
    }

    update()
    {
      this.service.getPage(this.pageNumber)
        .subscribe(x => 
          x.forEach(e => this.posts.push(e))
        )
      console.log(this.posts)
    }

    getFiltersString() : string
    {

      return ''
    }

    like(post: Post) {
      post.liked = !post.liked;
      
      this.service.like(post.idPost, post.liked)
    }

    moreContent = () => {
      this.pageNumber += 1;
      console.log(this.pageNumber)
      this.service.getPage(this.pageNumber)
    }
}
