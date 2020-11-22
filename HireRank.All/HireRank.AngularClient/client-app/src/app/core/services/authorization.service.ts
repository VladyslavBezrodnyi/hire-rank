import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { CurrentUser } from 'src/app/shared/models/current-user.model';
import { environment } from 'src/environments/environment';
// @ts-ignore
import jwt_decode from 'jwt-decode';
import { BehaviorSubject, Observable } from 'rxjs';
import { TokenResponse } from 'src/app/shared/models/token-response.model';
import { LoginModel } from 'src/app/shared/models/login.model';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationService {
  public currentUser: CurrentUser;
  private loggedIn = new BehaviorSubject<boolean>(false);
  private role = new BehaviorSubject<string>(null);

  constructor(private http: HttpClient, private router: Router) {
    let currentUserString = localStorage.getItem('currentUser');
    this.currentUser = currentUserString ? JSON.parse(currentUserString) : null;
    this.loggedIn.next(this.currentUser != null);
    this.role.next(this.currentUser?.role);
  }

  get isLoggedIn(): Observable<boolean> {
    return this.loggedIn.asObservable();
  }

  get UserRole(): Observable<string> {
    return this.role.asObservable();
  }

  login(loginModel: LoginModel): Observable<TokenResponse> {
    return this.http.post<TokenResponse>(environment.apiUrl + `/account/login`, loginModel)
      .pipe(map(tokenResponse => {
        let token = tokenResponse.accessToken;
        this.currentUser = this.getUserInfo(token, loginModel.email);
        localStorage.setItem('currentUser', JSON.stringify(this.currentUser));
        this.loggedIn.next(true);
        this.role.next(this.currentUser.role);
        return tokenResponse;
      }));
  }

  logout(): void {
    localStorage.removeItem('currentUser');
    this.currentUser = null;
    this.loggedIn.next(false);
    this.role.next(null);
  }

  getUserInfo(token: string, email: string): CurrentUser {
    let decodedToken = jwt_decode(token);
    let user = {
      id: decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'],
      email: email,
      token: token,
      role: decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
    } as CurrentUser;

    return user;
  }
}
