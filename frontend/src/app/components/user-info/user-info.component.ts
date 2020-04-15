import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/models/user.model';

@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.css']
})
export class UserInfoComponent implements OnInit {

  user: User = {
    id: 0,
    name: '',
    email: '',
    password: '',
    created_at: null,
    updated_at: null
  };

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.userService.readByToken().subscribe(user => {
      this.user = user;
    });
  }

}
