import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiErrorService } from './api-error.service';
import { Observable } from 'rxjs';
import { List } from '../models/list.model';
import { environment } from 'src/environments/environment';
import { map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ListService {

  constructor(private http: HttpClient,
    private apiErrorService: ApiErrorService) { }

  getAllLists(): Observable<List[]> {
    return this.http.get<List[]>(`${environment.apiUrl}/lists`).pipe(
      map((obj) => obj),
      catchError((e) => this.apiErrorService.apiErrorHandler(e))
    );
  }

  getListById(id: number): Observable<List> {
    return this.http.get<List>(`${environment.apiUrl}/lists/${id}`).pipe(
      map((obj) => obj),
      catchError((e) => this.apiErrorService.apiErrorHandler(e))
    );
  }

  addList(list: List): Observable<List> {
    return this.http.post<List>(`${environment.apiUrl}/lists`, list).pipe(
      map((obj) => obj),
      catchError((e) => this.apiErrorService.apiErrorHandler(e))
    );
  }

  updateList(list: List): Observable<List> {
    return this.http.put<List>(`${environment.apiUrl}/lists/${list.id}`, list).pipe(
      map((obj) => obj),
      catchError((e) => this.apiErrorService.apiErrorHandler(e))
    );
  }

  deleteList(id: number): Observable<any> {
    return this.http.delete<any>(`${environment.apiUrl}/lists/${id}`).pipe(
      map((obj) => obj),
      catchError((e) => this.apiErrorService.apiErrorHandler(e))
    );
  }
}
