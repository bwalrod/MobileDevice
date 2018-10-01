import { AlertifyService } from './../_services/alertify.service';
import { UserService } from './../_services/user.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Router, ActivatedRouteSnapshot, ActivatedRoute } from '@angular/router';
import { NgForm } from '@angular/forms';
import { User } from '../_models/user';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css']
})
export class UserEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;
  user: User;
  userName: string;
  newUser: User = {
    id: 0,
    firstName: '',
    lastName: '',
    login: '',
    accessLevel: 0,
    active: true
  };

  constructor(private userService: UserService, private router: Router, private alertify: AlertifyService
    , private route: ActivatedRoute) { }

  ngOnInit() {
    this.user = this.newUser;

    this.route.data.subscribe(data => {
      if (data['user']) {
        this.user = data['user'];
        this.userName = this.user.firstName + ' ' + this.user.lastName;
      }
    });
    // this.loadUser();
  }

  // loadUser() {
  //   return this.userService.getUser(+this.route.snapshot.params['id'])
  //     .subscribe((user: User) => {
  //       this.user = user;
  //     },
  //     error => {
  //       this.alertify.error(error);
  //     }
  //     );
  // }

  submitForm() {
    if (this.editForm.dirty) {
      if (this.user.id > 0) {
        this.updateUser();
      } else {
        this.insertUser();
      }
    }
  }

  updateUser() {
    if (this.editForm.dirty) {
    this.userService.updateUser(this.user)
      .subscribe(next => {
        this.alertify.success('User updated successfully');
        this.editForm.reset(this.user);
        this.userName = this.user.firstName + ' ' + this.user.lastName;
      }, error => {
        this.alertify.error(error);
      });
    }
  }

  insertUser() {
    this.userService.addUser(this.user)
      .subscribe((user: User) => {
        this.alertify.success('User added successfully');
        this.user = user;
        this.editForm.reset(this.user);
        this.router.navigate(['/users/edit/' + this.user.id]);
      }, error => {
        this.alertify.error(error);
      });
  }

  deactivateUser(id: number) {
    this.alertify.confirm('Are you sure you want to delete this user?', () => {
      this.userService.deactivateUser(id)
      .subscribe(() => {
        this.router.navigate(['/users']);
      }, error => {
        this.alertify.error('Failed to delete user');
      });
    });
  }

  returnToList() {
    this.router.navigate(['/users']);
  }
}
