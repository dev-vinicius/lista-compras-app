import { Injectable } from '@angular/core';
import { Observable, EMPTY } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { User } from '../models/user.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { MessageService } from './message.service';
import { ApiErrorService } from './api-error.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient,
    private messageService: MessageService,
    private apiErrorService: ApiErrorService) { }

  readByToken(): Observable<User> {
    return this.http.get<User>(`${environment.apiUrl}/users`).pipe(
      map((obj) => obj),
      catchError((e) => this.apiErrorService.apiErrorHandler(e))
    );
  }

}
