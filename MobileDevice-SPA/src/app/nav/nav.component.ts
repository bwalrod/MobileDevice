import { AlertifyService } from './../_services/alertify.service';
import { User } from './../_models/user';
import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  user: User = {
    id: 0,
    login: '',
    firstName: '',
    lastName: '',
    accessLevel: 0,
    active: false,
  };

  constructor(public userService: UserService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.identifyUser();
    // this.user = this.userService.currentUser;
  }

  identifyUser() {
    this.userService.identifyUser()
      .subscribe(next => {
        // this.alertify.message('identifyUser');
        this.user = this.userService.currentUser;
      }, error => {
        this.alertify.error(error);
      });
  }

}
