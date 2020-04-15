import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, EMPTY } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { MessageService } from './message.service';
import { User } from '../models/user.model';
import { environment } from '../../environments/environment';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private http: HttpClient,
    private router: Router,
    private messageService: MessageService) { }

  login(user: string, pass: string): Observable<any> {
    localStorage.clear();
    return this.http.post<any>(`${environment.apiUrl}/users/login`, { email: user, password: pass }).pipe(
      map((obj) => {
        localStorage.setItem(environment.tokenKey, obj.token);
        this.router.navigate(['']);
        return obj;
      }),
      catchError((e) => this.errorHandler(e))
    );
  }

  isLoggedIn(): boolean {
    const token = localStorage.getItem(environment.tokenKey);
    return token != null;
  }

  getToken(): string {
    return localStorage.getItem(environment.tokenKey);
  }

  errorHandler(e: any): Observable<any> {
    this.messageService.showMessage(e.error.error, true);
    return EMPTY;
  }
}
