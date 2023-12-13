import {Injectable} from "@angular/core";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {AccountUpdate, Credentials, Registration, User} from "../app/models";




@Injectable({providedIn: "root"})
export class AccountService {
  constructor(private readonly http: HttpClient) {
  }

  getCurrentUser() {
    let token = sessionStorage.getItem('token')
    console.log(token)
    let headers = new HttpHeaders( {
      authorization: 'Bearer '+sessionStorage.getItem('token')!
    })
    return this.http.get<User>('http://localhost:5280/api/users/whoami', {headers: headers})
  }

  login(value: Credentials) {
    return this.http.post<{ token: string }>('http://localhost:5280/api/users/login', value);
  }

  register(value: Registration) {
    return this.http.post<any>('/api/users', value);
  }

  update(value: AccountUpdate) {
    const formData = new FormData();
    Object.entries(value).forEach(([key, value]) =>
      formData.append(key, value)
    );
    return this.http.put<User>('/api/users/'+ "id" + formData, {
      reportProgress: true,
      observe: 'events'
    });
  }
}
