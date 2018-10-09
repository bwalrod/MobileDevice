import { Router, ActivatedRoute } from '@angular/router';
import { AlertifyService } from './../_services/alertify.service';
import { DeviceStatusService } from './../_services/devicestatus.service';
import { Pagination, PaginatedResult } from './../_models/pagination';
import { DeviceStatus } from './../_models/DeviceStatus';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-devicestatus-list',
  templateUrl: './devicestatus-list.component.html',
  styleUrls: ['./devicestatus-list.component.css']
})
export class DevicestatusListComponent implements OnInit {

  list: DeviceStatus[];
  pagination: Pagination;
  filter = '';
  status = 'Active';
  elementLabel = 'device status';
  pageLabel = 'Device Statuses';
  pageRoute = 'devicestatuses';

  constructor(private service: DeviceStatusService, private alertify: AlertifyService
                , private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.list = data['devicestatuses'].result;
      this.pagination = data['devicestatuses'].pagination;
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
    this.service.getDeviceStatuses(this.pagination.currentPage, this.pagination.itemsPerPage,
      this.filter, activeStatus)
      .subscribe((res: PaginatedResult<DeviceStatus[]>) => {
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
      this.service.deactivateDeviceStatus(id)
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

