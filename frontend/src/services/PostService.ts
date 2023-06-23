import { Injectable } from '@angular/core';
import { CompletePost } from './Post';
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

}
