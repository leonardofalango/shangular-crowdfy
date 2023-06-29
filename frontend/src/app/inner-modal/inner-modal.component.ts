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
    FormsModule]
})
export class InnerModalComponent 
  implements OnInit{
  constructor(
    private service: ForumService,
    private postService: PostService) {  }


  myControl = new FormControl<string | Forum>('');
  options: Forum[] = [];
  

  filteredOptions!: Observable<Forum[]>;
  userId: string | null = sessionStorage.getItem("userId");

  selectOption: string | null = null;

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
      archive: ''
    }


  ngOnInit() {
    this.service.getSubscribedForums(this.userId).subscribe(x => this.options = x);
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

    console.log(this.selectOption);
    console.log(this.selectOption);
    console.log(this.selectOption);
    console.log(this.selectOption);
    console.log(this.selectOption);
    console.log(this.selectOption);
    
    this.service
      .getForumByName(this.selectOption)
      .subscribe(x =>
      {
        this.post.forumName = x.title
        
        this.postService
          .postPost(this.post)
      })
  }
}
