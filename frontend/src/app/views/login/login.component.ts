import { Component, OnInit } from '@angular/core';
import { LoginService } from 'src/app/services/login.service';
import { User } from 'src/app/models/user.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private loginService: LoginService) { }

  user: User = {
    id: 0,
    name: '',
    email: '',
    password: '',
    created_at: null,
    updated_at: null
  };
  loading = false;

  ngOnInit(): void {
  }

  login(): void {
    this.loading = true;
    this.loginService.login(this.user.email, this.user.password).subscribe(null, null, () => {
      this.loading = false;
    });
  }
}
