import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";


export interface Credentials {
  email: string;
  password: string;
}

export interface User {
  userid: number;
  username: string;
  email: string;
  avatarUrl: string | null;
  isAdmin: boolean;
}

export interface Registration {
  username: string;
  email: String;
  password: string;
}

export interface AccountUpdate {
  username: string;
  email: string;
  avatar: File | null;
}

@Injectable({providedIn: "root"})
export class AccountService {
  constructor(private readonly http: HttpClient) {
  }

  getCurrentUser() {
    return this.http.get<User>('/api/users/whoami');
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
