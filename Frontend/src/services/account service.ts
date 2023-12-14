import {Injectable} from "@angular/core";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import { Credentials, Registration, User} from "../app/models";
import {firstValueFrom} from "rxjs";




@Injectable({providedIn: "root"})
export class AccountService {
  constructor(private readonly http: HttpClient) {
  }

  getCurrentUser() {
    let headers = new HttpHeaders( {
      authorization: 'Bearer '+sessionStorage.getItem('token')!
    })
    return this.http.get<User>('http://localhost:5280/api/users/whoami', {headers: headers})
  }

  login(value: Credentials) {
    return this.http.post<{ token: string }>('http://localhost:5280/api/users/login', value);
  }

  register(value: Registration) {
    return this.http.post<any>('http://localhost:5280/api/users', value);
  }

  async update(value: User) {
    return this.http.put<User>('http://localhost:5280/api/users/'+value.userId, value, {
    });
  }
  async updateAvatar(value: File) {

    var user:User = await firstValueFrom(this.getCurrentUser());

    const formData = new FormData();
    formData.append('avatar', value);

    return this.http.put<User>('http://localhost:5280/api/users/' + user.userId + '/avatar', formData, {
    });
  }
  async getUserRecipes() {
    var user:User = await firstValueFrom(this.getCurrentUser());
    return this.http.get<any>('http://localhost:5280/api/recipes/' + user.userId, {
    });
  }


}
