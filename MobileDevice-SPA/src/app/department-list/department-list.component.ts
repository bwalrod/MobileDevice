import { DepartmentService } from './../_services/department.service';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from './../_services/alertify.service';
import { Pagination, PaginatedResult } from './../_models/pagination';
import { Department } from './../_models/department';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-department-list',
  templateUrl: './department-list.component.html',
  styleUrls: ['./department-list.component.css']
})
export class DepartmentListComponent implements OnInit {

  departments: Department[];
  pagination: Pagination;
  filter = '';

  constructor(private departmentService: DepartmentService, private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.departments = data['departments'].result;
      this.pagination = data['departments'].pagination;
      this.filter = null;
    });
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadDepartments();
  }

  loadDepartments() {
    this.departmentService.getDepartments(this.pagination.currentPage, this.pagination.itemsPerPage, this.filter)
    .subscribe((res: PaginatedResult<Department[]>) => {
      this.departments = res.result;
      this.pagination = res.pagination;
    });
  }

  filterTable() {
    this.pagination.currentPage = 1;
    this.pagination.itemsPerPage = 5;
    this.loadDepartments();
  }

}
