import { Injectable } from '@angular/core';
import {User} from "../app/models";
import {firstValueFrom} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {environment} from "../environments/environment";

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
    const call = this.http.get<User[]>(environment.baseUrl +'/api/users');
    this.users = await firstValueFrom<User[]>(call);
  }

  getUserByIdFromList(userId: number | undefined): User | undefined {
    return this.users.find(user => user.userId === userId);
  }
}
