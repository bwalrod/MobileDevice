import { PaginatedResult } from './../_models/pagination';
import { Router, ActivatedRoute } from '@angular/router';
import { AlertifyService } from './../_services/alertify.service';
import { AssigneeService } from './../_services/assignee.service';
import { Assignee } from './../_models/assignee';
import { Component, OnInit } from '@angular/core';
import { Pagination } from '../_models/pagination';

@Component({
  selector: 'app-assignee-list',
  templateUrl: './assignee-list.component.html',
  styleUrls: ['./assignee-list.component.css']
})
export class AssigneeListComponent implements OnInit {


  list: Assignee[];
  pagination: Pagination;
  filter: any = {};
  status = 'Active';
  elementLabel = 'assignee';
  pageLabel = 'Assignees';
  pageRoute = 'assignees';
  sub;
  qDepartmentId = -1;

  constructor(private service: AssigneeService, private alertify: AlertifyService,
                private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.list = data['assignees'].result;
      this.pagination = data['assignees'].pagination;

      this.sub = this.route.queryParams.subscribe(params => {
        this.qDepartmentId = +params['departmentId'] || 0;
        console.log(this.qDepartmentId);
      });
    });


    this.filter.firstName = '';
    this.filter.lastName = '';
    this.filter.departmentId = this.qDepartmentId;
  }

  loadList() {
    let activeStatus = 2;
    if (this.status === 'Active') {
      activeStatus = 1;
    }
    if (this.status === 'Inactive') {
      activeStatus = 0;
    }
    console.log(JSON.stringify(this.filter));
    this.service.getAssignees(this.pagination.currentPage, this.pagination.itemsPerPage,
      this.filter, activeStatus)
      .subscribe((res: PaginatedResult<Assignee[]>) => {
        this.list = res.result;
        this.pagination = res.pagination;
      });
  }

  filterTable() {
    this.pagination.currentPage = 1;
    this.pagination.itemsPerPage = 10;
    this.loadList();
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadList();
  }

  deactivateElement(id: number) {
    this.alertify.confirm('Are you sure you want to delete this ' + this.elementLabel + '?', () => {
      this.service.deactivateAssignee(id)
        .subscribe(() => {
          this.loadList();
        }, error => {
          this.alertify.error('Failed to delete ' + this.elementLabel);
        });
    });
  }

  filterByDepartment(deptFilter: number) {
    this.filter.departmentId = deptFilter;
    this.filterTable();
  }

  clearFilter() {
    this.filter.firstName = '';
    this.filter.lastName = '';
    this.filter.departmentId = 0;
    this.filterTable();
  }
}
