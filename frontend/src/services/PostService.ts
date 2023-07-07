import { Injectable } from '@angular/core';
import { Post } from './Post';
import { HttpClient } from '@angular/common/http'

@Injectable({
  providedIn: 'root'
})
export class PostService {

  constructor(private http: HttpClient) { }

  getPage(filters: string, page: number)
  {
    return this.http.get<Post[]>("http://localhost:5177/post/" + filters + '-' + page);
  }

  postPost(post: Post)
  {
    return this.http.post("http://localhost:5177/post/add", post);
  }

  like(idPost: number, liked: boolean)
  {
    return this.http.post("http://localhost:5177/post/like", {
      idUser: sessionStorage.getItem("userId"),
      idPost: idPost,
      liked: liked
    });


}
