import { Pagination, PaginatedResult } from './../_models/pagination';
import { User } from './../_models/user';
import { AlertifyService } from './../_services/alertify.service';
import { UserService } from './../_services/user.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  users: User[];
  pagination: Pagination;
  userParams: any = {};
  status = 'Active';

  constructor(private userService: UserService, private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    // this.loadUsers();
    this.route.data.subscribe(data => {
      this.users = data['users'].result;
      this.pagination = data['users'].pagination;
    });

    this.userParams.login = '';
    this.userParams.firstname = '';
    this.userParams.lastname = '';
    this.userParams.accesslevel = '';
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadUsers();
  }

  loadUsers() {
    let activeStatus = 2;
    if (this.status === 'Active') {
      activeStatus = 1;
    }
    if (this.status === 'Inactive') {
      activeStatus = 0;
    }
    this.userService.getUsers(this.pagination.currentPage, this.pagination.itemsPerPage, this.userParams, activeStatus)
      .subscribe((res: PaginatedResult<User[]>) => {
        this.users = res.result;
        this.pagination = res.pagination;
      },
    // this.userService.getUsers()
    // .subscribe((users: User[]) => {
    //   this.users = users;
    // },
    error => {
      this.alertify.error(error);
    });
  }

  filterTable() {
    this.pagination.currentPage = 1;
    this.pagination.itemsPerPage = 5;
    this.loadUsers();
  }

  deactivateUser(id: number) {
    this.alertify.confirm('Are you sure you want to delete this user?', () => {
      this.userService.deactivateUser(id)
        .subscribe(() => {
          this.loadUsers();
        }, error => {
          this.alertify.error('Failed to delete user');
        });
    });
  }

}
