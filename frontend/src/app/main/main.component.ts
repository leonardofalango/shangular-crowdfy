import { Component, OnInit } from '@angular/core';
import { CompletePost } from '../../services/Post'
import { PostService } from 'src/services/PostService';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})

export class MainComponent 
  implements OnInit{

    posts: CompletePost[] = [];
    pageNumber: number = 0;
    
    constructor(private service: PostService) { }

    ngOnInit(): void {
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
