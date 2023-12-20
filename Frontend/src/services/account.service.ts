import {Injectable} from "@angular/core";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Credentials, Registration, User} from "../app/models";
import {firstValueFrom} from "rxjs";
import {environment} from "../environments/environment";




@Injectable({providedIn: "root"})
export class AccountService {
  constructor(private readonly http: HttpClient) {
  }

  getCurrentUser() {
    let headers = new HttpHeaders( {
      authorization: 'Bearer '+sessionStorage.getItem('token')!
    })
    return this.http.get<User>(environment.baseUrl +'/api/users/whoami', {headers: headers})
  }

  login(value: Credentials) {
    return this.http.post<{ token: string }>(environment.baseUrl +'/api/users/login', value);
  }

  register(value: Registration) {
    return this.http.post<any>(environment.baseUrl +'/api/users', value);
  }

  async update(value: User) {
    return this.http.put<User>(environment.baseUrl +'/api/users/'+value.userId, value, {
    });
  }
  async updateAvatar(value: File) {
    var user:User = await firstValueFrom(this.getCurrentUser());

    const formData = new FormData();
    formData.append('avatar', value);
    return this.http.put<User>(environment.baseUrl +'/api/users/' + user.userId + '/avatar', formData, {
    });
  }
}
