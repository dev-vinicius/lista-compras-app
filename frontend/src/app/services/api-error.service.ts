import { Injectable } from '@angular/core';
import { Observable, EMPTY } from 'rxjs';
import { MessageService } from './message.service';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ApiErrorService {

  constructor(private router: Router,
    private messageService: MessageService) { }

  apiErrorHandler(e: any): Observable<any> {
    if (e.status == 404 || e.status == 400 || e.status == 500) {
      this.messageService.showMessage(`${e.error.error}...`, true);
    }else if (e.status == 401) {
      localStorage.removeItem(environment.tokenKey);
      this.messageService.showMessage("É necessário fazer o login...", true);
      this.router.navigate([""]);
    }
    return EMPTY;
  }
}
