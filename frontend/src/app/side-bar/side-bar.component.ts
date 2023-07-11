import { Component, OnInit } from '@angular/core';
import { User } from '../../services/User'
import { UserService } from '../../services/UserService'
import { ForumService } from 'src/services/ForumService';
import { Forum } from 'src/services/Forum';
import { AthenticateService } from 'src/services/AthenticateService';

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
      username: '',
      photo: '',
      bornDate: new Date,
      mail : '',
      isAuth : 0
    };

    forums: Forum[] = []
    userId: string | null = sessionStorage.getItem("userId");

    auth: string = ''

    constructor(
      private authe : AthenticateService,
      private forumService: ForumService,
      private userService: UserService
    ) { }

    ngOnInit(): void {
    
      if (!sessionStorage.getItem("userId"))
      {
        location.reload();
      }

      this.userService
        .getUser(
          sessionStorage.getItem('userId')
        )
        .subscribe(u => {
          this.user = u;
          console.log(u);
        })
      
        
      this.user.isAuth == 0?
        this.auth = "User Comum"
        : this.auth = "Adimininastro"
      
      this.forumService
        .getSubscribedForums(sessionStorage.getItem('userId'))
        .subscribe(
          x => this.forums = x
        )
    }
    
}
