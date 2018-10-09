import { DeviceDateType } from './../_models/devicedatetype';
import { Component, OnInit } from '@angular/core';
import { Pagination, PaginatedResult } from '../_models/pagination';
import { DeviceDateTypeService } from '../_services/devicedatetype.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-devicedatetype-list',
  templateUrl: './devicedatetype-list.component.html',
  styleUrls: ['./devicedatetype-list.component.css']
})
export class DevicedatetypeListComponent implements OnInit {

  list: DeviceDateType[];
  pagination: Pagination;
  filter = '';
  status = 'Active';
  elementLabel = 'device date type';
  pageLabel = 'Device Date Types';
  pageRoute = 'devicedatetypes';

  constructor(private service: DeviceDateTypeService, private alertify: AlertifyService
                , private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.list = data['devicedatetypes'].result;
      this.pagination = data['devicedatetypes'].pagination;
      this.filter = null;
    });
  }

  loadList() {
    let activeStatus = 2;
    if (this.status === 'Active') {
      activeStatus = 1;
    }
    if (this.status === 'Inactive') {
      activeStatus = 0;
    }
    this.service.getDeviceDateTypes(this.pagination.currentPage, this.pagination.itemsPerPage,
      this.filter, activeStatus)
      .subscribe((res: PaginatedResult<DeviceDateType[]>) => {
        this.list = res.result;
        this.pagination = res.pagination;
      });
  }

  filterTable() {
    this.pagination.currentPage = 1;
    this.pagination.itemsPerPage = 5;
    this.loadList();
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadList();
  }

  deactivateElement(id: number) {
    this.alertify.confirm('Are you sure you want to delete this ' + this.elementLabel + '?', () => {
      this.service.deactivateDeviceDateType(id)
        .subscribe(() => {
          this.loadList();
        }, error => {
          this.alertify.error('Failed to delete ' + this.elementLabel);
        });
    });
  }

  resetFilter() {
    this.filter = '';
    this.filterTable();
  }
}

