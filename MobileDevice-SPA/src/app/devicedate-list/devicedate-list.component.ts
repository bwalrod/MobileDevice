import { AlertifyService } from './../_services/alertify.service';
import { DevicedateService } from './../_services/devicedate.service';
import { Pagination, PaginatedResult } from './../_models/pagination';
import { DeviceDate } from './../_models/devicedate';
import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-devicedate-list',
  templateUrl: './devicedate-list.component.html',
  styleUrls: ['./devicedate-list.component.css']
})
export class DeviceDateListComponent implements OnInit {
  @Input() deviceId: number;
  list: DeviceDate[];
  pagination: Pagination;
  userParams: any = {};
  status = 'Active';
  elementLabel = 'device date';
  pageLabel = 'Device Dates';
  pageRoute = 'devicedates';
  pageNumber = 1;
  pageSize = 5;

  constructor(private service: DevicedateService, private alertify: AlertifyService
                , private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    // this.userParams.dateValue = '0001-01-01';
    this.userParams.deviceId = this.deviceId;
    this.service.getDeviceDates(this.pageNumber, this.pageSize, this.userParams, 1)
    .subscribe((res: PaginatedResult<DeviceDate[]>) => {
      this.list = res.result;
      this.pagination = res.pagination;
    });
    // this.userParams.deviceId = this.deviceId;
    // this.pagination = new Pagination();
    // this.pagination.currentPage = 1;
    // this.pagination.itemsPerPage = 5;
    // this.loadList();
    // this.route.data.subscribe(data => {
    //   this.list = data['devicedates'].result;
    //   this.pagination = data['devicedates'].pagination;
    // });
  }

  loadList() {
    let activeStatus = 2;
    if (this.status === 'Active') {
      activeStatus = 1;
    } else if (this.status === 'Inactive') {
      activeStatus = 0;
    }
    this.service.getDeviceDates(this.pagination.currentPage, this.pagination.itemsPerPage,
        this.userParams, activeStatus)
        .subscribe((res: PaginatedResult<DeviceDate[]>) => {
          this.list = res.result;
          this.pagination = res.pagination;
        });
  }

}
