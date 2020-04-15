import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor(private snackbar: MatSnackBar) { }

  showMessage(message: string, isError: boolean = false): void {
    this.snackbar.open(message, 'X', {
      duration: 3000,
      horizontalPosition: "center",
      verticalPosition: "bottom",
      panelClass: isError ? ["msg-error"] : ["msg-success"]
    });
  }
}
