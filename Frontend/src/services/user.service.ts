import { Injectable } from '@angular/core';
import {Recipe, User} from "../app/models";
import {firstValueFrom} from "rxjs";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  public users: User[] = [];
  public currentUser: User = {};

  constructor(private http : HttpClient) {
    this.getUsers();
  }

  async getUsers(){
    const call = this.http.get<User[]>('http://localhost:5280/api/users');
    this.users = await firstValueFrom<User[]>(call);
  }

  getUserById(userId: number | undefined): User | undefined {
    return this.users.find(user => user.userId === userId);
  }
}
