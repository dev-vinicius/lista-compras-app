import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiErrorService } from './api-error.service';
import { environment } from 'src/environments/environment';
import { map, catchError } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { Item } from '../models/item.model';

@Injectable({
  providedIn: 'root'
})
export class ItemService {

  constructor(private http: HttpClient,
    private apiErrorService: ApiErrorService) { }

  getItemsByList(listId: number): Observable<Item[]> {
    return this.http.get<Item[]>(`${environment.apiUrl}/itens?list_id=${listId}`).pipe(
      map((obj) => obj),
      catchError((e) => this.apiErrorService.apiErrorHandler(e))
    );
  }

  getItemById(id: number): Observable<Item> {
    return this.http.get<Item>(`${environment.apiUrl}/itens/${id}`).pipe(
      map((obj) => obj),
      catchError((e) => this.apiErrorService.apiErrorHandler(e))
    );
  }

  addItem(item: Item): Observable<Item> {
    return this.http.post<Item>(`${environment.apiUrl}/itens`, item).pipe(
      map((obj) => obj),
      catchError((e) => this.apiErrorService.apiErrorHandler(e))
    );
  }

  updateItem(item: Item): Observable<Item> {
    return this.http.put<Item>(`${environment.apiUrl}/itens/${item.id}`, item).pipe(
      map((obj) => obj),
      catchError((e) => this.apiErrorService.apiErrorHandler(e))
    );
  }

  deleteItem(id: number): Observable<any> {
    return this.http.delete<any>(`${environment.apiUrl}/itens/${id}`).pipe(
      map((obj) => obj),
      catchError((e) => this.apiErrorService.apiErrorHandler(e))
    );
  }
}
