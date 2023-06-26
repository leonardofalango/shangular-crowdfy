import { Component, OnInit } from '@angular/core';
import { User } from '../../services/User'
import { UserService } from '../../services/UserService'
import { ForumService } from 'src/services/ForumService';
import { Forum } from 'src/services/Forum';

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.css']
})
export class SideBarComponent 
  implements OnInit{
    user: User = 
    {
      id : 0,
      completeName : '',
      userName: '',
      photo: '',
      bornDate: new Date,
      mail : '',
      isAuth : 0
    };

    forums: Forum[] = []

    auth: string = ''

    constructor(
      private userService : UserService,
      private forumService: ForumService
    ) { }

    ngOnInit(): void {
      // this
      //   .userService
      //   .getUser(sessionStorage
      //       .getItem('userId'))
      //   .subscribe(x => this.user)
        
      this.user.isAuth == 0?
        this.auth = "User Comum"
        : this.auth = "Adimininastro"
      
      this
        .forumService
        .getSubscribedForums(sessionStorage.getItem('userId'))
        .subscribe(
          x => this.forums = x
        )
    }
}
