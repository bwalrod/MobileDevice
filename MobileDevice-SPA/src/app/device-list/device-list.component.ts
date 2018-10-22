import { PaginatedResult } from 'src/app/_models/pagination';
import { Router, ActivatedRoute } from '@angular/router';
import { AlertifyService } from './../_services/alertify.service';
import { Pagination } from './../_models/pagination';
import { Device } from './../_models/device';
import { Component, OnInit } from '@angular/core';
import { DeviceService } from '../_services/device.service';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-device-list',
  templateUrl: './device-list.component.html',
  styleUrls: ['./device-list.component.css']
})
export class DeviceListComponent implements OnInit {

  list: Device[];
  pagination: Pagination;
  filter = '';
  userParams: any = {};
  status = 'Active';
  elementLabel = 'device';
  pageLabel = 'Devices';
  pageRoute = 'devices';
  sub;
  qManufacturerId = 0;
  qProductTypeId = 0;
  qAssigneeId = 0;

  constructor(private service: DeviceService, private alertify: AlertifyService
                , private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.list = data['devices'].result;
      this.pagination = data['devices'].pagination;
      // this.filter = null;
      this.sub = this.route.queryParams.subscribe(params => {
        this.qManufacturerId = params['manufacturerId'] || 0;
        this.qProductTypeId = params['productTypeId'] || 0;
        console.log('DeviceList - ' + this.qManufacturerId.toString());
      });
    });

    this.userParams.serialNumber = '';
    this.userParams.esn = '';
    this.userParams.os = '';
    this.userParams.productTypeId = this.qProductTypeId;
    this.userParams.productManufacturerId = this.qManufacturerId;
    this.userParams.assigneeId = this.qAssigneeId;
  }

  loadList() {
    let activeStatus = 2;
    if (this.status === 'Active') {
      activeStatus = 1;
    }
    if (this.status === 'Inactive') {
      activeStatus = 0;
    }
    this.service.getDevices(this.pagination.currentPage, this.pagination.itemsPerPage,
      this.userParams, activeStatus)
      .subscribe((res: PaginatedResult<Device[]>) => {
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
      this.service.deactivateDevice(id)
        .subscribe(() => {
          this.loadList();
        }, error => {
          this.alertify.error('Failed to delete ' + this.elementLabel);
        });
    });
  }

  filterByType(typeFilter: number) {
    this.userParams.productTypeId = typeFilter;
    this.filterTable();
  }

  filterByManufacturer(manufacturerFilter: number) {
    this.userParams.productManufacturerId = manufacturerFilter;
    this.filterTable();
  }

  filterByModel(modelFilter: number) {
    this.userParams.productModelId = modelFilter;
    this.filterTable();
  }

  filterByDepartment(deptFilter: number) {
    this.userParams.assigneeDepartmentId = deptFilter;
    this.filterTable();
  }

  filterByCapacity(capacityFilter: number) {
    this.userParams.productCapacityId = capacityFilter;
    this.filterTable();
  }

  filterByAssignee(assigneeFilter: number) {
    this.userParams.assigneeId = assigneeFilter;
    this.filterTable();
  }

  clearFilter() {
    this.userParams.serialNumber = '';
    this.userParams.esn = '';
    this.userParams.os = '';
    this.userParams.productTypeId = 0;
    this.userParams.productManufacturerId = 0;
    this.userParams.productModelId = 0;
    this.userParams.assigneeDepartmentId = 0;
    this.userParams.productCapacityId = 0;
    this.userParams.assigneeId = 0;
    this.filterTable();
  }

  makeAssigneeName(lastName: string, firstName: string) {
    if (lastName != null && firstName != null) {
      return lastName + ', ' + firstName;
    } else {
      return '';
    }
  }

}

