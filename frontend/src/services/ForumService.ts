import { Forum } from './Forum'; 
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { FocusTrap } from '@angular/cdk/a11y';

@Injectable({
  providedIn: 'root'
})
export class ForumService {

  constructor(private http: HttpClient) { }

  getAllForums = () => this.http.get<Forum[]>("http://localhost:5177/forum")
  

  getSubscribedForums = (id: string | null) => this.http.get<Forum[]>("http://localhost:5177/forum/get/" + id)
  

  getForumByName = (name: string | null) => this.http.get<Forum>("http://localhost:5177/forum/getByname/" + name)
  

  addForum = (forum: Forum) => this.http.post("https://localhost:5177/forum/add", forum)

}
