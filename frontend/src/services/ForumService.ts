import { Forum } from './Forum'; 
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { User } from './User';

@Injectable({
  providedIn: 'root'
})
export class ForumService {

  constructor(private http: HttpClient) { }

  getAllForums = () => this.http.get<Forum[]>("http://localhost:5177/forum")
  

  getSubscribedForums = (idUser: string | null) => this.http.get<Forum[]>("http://localhost:5177/forum/getSubscribedForums/" + idUser)
  
  //! Decapreted
  getAll = () => this.http.get<Forum[]>("http://localhost:5177/forum/")
  
  //! ?????
  getForumByName = (name: string | null) => this.http.get<Forum>("http://localhost:5177/forum/getByname/" + name)
  

  addForum = (forum: Forum) => this.http.post("https://localhost:5177/forum/add", forum)

  delete = (forum: Forum) => this.http.post("https://localhost:5177/forum/delete", forum)

  update = (forum: Forum) => this.http.post("https://localhost:5177/forum/update", forum)

}
