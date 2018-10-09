import { DepartmentService } from './../_services/department.service';
import { ActivatedRoute, Router } from '@angular/router';
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
  status = 'Active';
  $: any;

  constructor(private departmentService: DepartmentService, private alertify: AlertifyService
    , private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    localStorage.setItem('departmentActiveFilter', this.status);
    this.route.data.subscribe(data => {
      this.departments = data['departments'].result;
      this.pagination = data['departments'].pagination;
      this.filter = null;
      // this.status = 1;
    });
  }


  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadDepartments();
  }

  loadDepartments() {
    let activeStatus = 2;
    if (this.status === 'Active') {
      activeStatus = 1;
    }
    if (this.status === 'Inactive') {
      activeStatus = 0;
    }
    this.departmentService.getDepartments(this.pagination.currentPage, this.pagination.itemsPerPage, this.filter, activeStatus)
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

  deactivateDepartment(id: number) {
    this.alertify.confirm('Are you sure you want to delete this department?', () => {
      this.departmentService.deactivateDepartment(id)
      .subscribe(() => {
        // this.router.navigate(['/departments']);
        this.loadDepartments();
      }, error => {
        this.alertify.error('Failed to delete department');
      });
    });
  }

  resetFilter() {
    this.filter = '';
    this.filterTable();
  }
}
