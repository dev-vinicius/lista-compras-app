import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, EMPTY } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { MessageService } from './message.service';
import { environment } from '../../environments/environment';
import { Router, ActivatedRoute } from '@angular/router';
import { ApiErrorService } from './api-error.service';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private http: HttpClient,
    private router: Router,
    private route: ActivatedRoute,
    private messageService: MessageService,
    private apiErrorService: ApiErrorService) { 

      setInterval(() => { 
        if (!this.isLoggedIn() && !(this.route.snapshot.children[0].routeConfig.path.includes('login'))){
          this.logout();
        }
      }, 3000);
    }

  login(user: string, pass: string): Observable<any> {
    localStorage.clear();
    return this.http.post<any>(`${environment.apiUrl}/users/login`, { email: user, password: pass }).pipe(
      map((obj) => {
        localStorage.setItem(environment.tokenKey, obj.access_token);
        this.router.navigate(['']);
        return obj;
      }),
      catchError((e) => this.apiErrorService.apiErrorHandler(e))
    );
  }

  logout(): void {
    localStorage.clear();
    this.messageService.showMessage("É necessário fazer o login...", true);
    this.router.navigate(["/login"]);
  }

  isLoggedIn(): boolean {
    const token = localStorage.getItem(environment.tokenKey);
    return token != null;
  }

  getToken(): string {
    return localStorage.getItem(environment.tokenKey);
  }
}
