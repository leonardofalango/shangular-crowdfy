import { Forum } from './Forum'; 
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'

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
  getForumByName = (name: string | null, userId : string | null) => this.http.post<Forum[]>("http://localhost:5177/forum/getByName/" + name, {
    id: userId
  })

  follow = (idForum: string, idUser: string | null) => this.http.post("https://localhost:5177/forum/subscribe", {
    idForum : idForum,
    idUser : idUser
  })
  

  addForum = (forum: Forum) => this.http.post("https://localhost:5177/forum/add", forum)

  delete = (forum: Forum) => this.http.post("https://localhost:5177/forum/delete", forum)

  update = (forum: Forum) => this.http.post("https://localhost:5177/forum/update", forum)

}
