import { Forum } from './Forum'; 
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'

@Injectable({
  providedIn: 'root'
})
export class ForumService {

  constructor(private http: HttpClient) { }

  getAllForums()
  {
    return this.http.get<Forum[]>("link")
  }

}
