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

  constructor(private userService: UserService, private router: Router, private alertify: AlertifyService
    , private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data['user'];
      this.userName = this.user.firstName + ' ' + this.user.lastName;
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
