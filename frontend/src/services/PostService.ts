import { Injectable } from '@angular/core';
import { CompletePost, Post } from './Post';
import { HttpClient } from '@angular/common/http'

@Injectable({
  providedIn: 'root'
})
export class PostService {

  constructor(private http: HttpClient) { }

  getPage(page: number)
  {
    return this.http.get<CompletePost[]>("http://localhost:5177/page/" + page);
  }

  postPost(post: Post)
  {
    return this.http.post("http://localhost:5177/post/add", post);
  }

}
