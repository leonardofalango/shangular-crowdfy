import { Component, OnInit } from '@angular/core';
import { CompletePost } from '../../services/Post'
import { PostService } from 'src/services/PostService';
import { UserService } from '../../services/UserService'
import { User } from 'src/services/User';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})

export class MainComponent 
  implements OnInit{

    posts: CompletePost[] = [];
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
    
    constructor(
      private service: PostService,
      private userService: UserService) { }

    ngOnInit(): void {
      this
        .userService
        .getUser(
          sessionStorage.getItem("userId")
        ).subscribe(
        x => this.user = x
      )
      
      this.update(this.pageNumber);
    }

    update(page : number)
    {
      this.service.getPage(page)
        .subscribe(x => 
          x.forEach(e => this.posts.push(e))
        )
      console.log(this.posts)
      
      this.pageNumber += 1;
    }
}
