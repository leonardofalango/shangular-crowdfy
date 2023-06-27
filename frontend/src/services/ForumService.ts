import { Forum } from './Forum'; 
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { FocusTrap } from '@angular/cdk/a11y';

@Injectable({
  providedIn: 'root'
})
export class ForumService {

  constructor(private http: HttpClient) { }

  getAllForums()
  {
    return this.http.get<Forum[]>("http://localhost:5177/forum")
  }

  getSubscribedForums(id: string | null)
  {
    return this.http.get<Forum[]>("http://localhost:5177/forum/" + id)
  }

  getForumByName(name: string | null)
  {
    return this.http.get<Forum>("http://localhost:5177/forum/name/" + name)
  }

}
