import { Component, OnInit } from '@angular/core';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { AsyncPipe, NgFor } from '@angular/common';
import { Observable, map, startWith } from 'rxjs';
import { Forum } from 'src/services/Forum';
import { ForumService } from 'src/services/ForumService';
import { Post } from 'src/services/Post';
import { FormsModule } from '@angular/forms'
import { PostService } from 'src/services/PostService';
import { UserService } from 'src/services/UserService';
import { Router } from '@angular/router';
import { AthenticateService } from 'src/services/AthenticateService';
import { User } from 'src/services/User';
import { MatSelectModule } from '@angular/material/select';


@Component({
  selector: 'app-inner-modal',
  templateUrl: './inner-modal.component.html',
  styleUrls: ['./inner-modal.component.css'],
  standalone: true,
  imports: [
    MatDialogModule,
    MatButtonModule,
    MatInputModule,
    MatAutocompleteModule,
    ReactiveFormsModule,
    NgFor,
    AsyncPipe,
    FormsModule,
    MatSelectModule]
})
export class InnerModalComponent 
  implements OnInit{
  constructor(
    private service: ForumService,
    private postService: PostService,
    private userService: UserService,
    private router: Router,
    private auth: AthenticateService
    ) {  }


  myControl = new FormControl<string | Forum>('');
  options: Forum[] = [];

  option: string = '';
  

  filteredOptions!: Observable<Forum[]>;

  //! USER ID
  userId: string = '1';

  selectOption: Forum =
  {
    id: 0,
    createdAt: new Date(),
    title: '',
    description: '',
    photo: '',
    selected: false
  }

  post: Post =
    {
      authorName: '',
      title: '',
      content: '',
      createdAt: new Date,
      crowds: 0,
      comments: 0,
      forumName: '',
      idPost: 0,
      photo: '',
      archive: '',
      liked: false
    }


  ngOnInit() {
    const jwt: string | null = sessionStorage.getItem('jwt');

    this.service.getAll()
      .subscribe(
        subscribedForumList =>
        this.options = subscribedForumList
      );


    this.filteredOptions = this.myControl.valueChanges.pipe(
      startWith(''),
      map(value => {
        const name = typeof value === 'string' ? value : value?.title;
        return name ? this._filter(name as string) : this.options.slice();
      }),
    );

  }

  displayFn(user: Forum): string {
    return user && user.title ? user.title : '';
  }

  private _filter(name: string): Forum[] {
    const filterValue = name.toLowerCase();

    return this.options.filter(option => option.title.toLowerCase().includes(filterValue));
  }

  pub(): void {
    const user: User =
    {
      id: 0,
      completeName: '',
      username: '',
      photo: '',
      bornDate: new Date(),
      mail: '',
      isAuth: 0
    }

    this.auth.authenticate(
      sessionStorage.getItem('jwtAuthenticator')
    )
    this.post.authorName = this.auth.user.username

    this.post.forumName = this.option
 

    console.log(
      this.post
    )
    this.postService.postPost(
      this.post
    )
  }
}
