import { Router, ActivatedRoute } from '@angular/router';
import { Pagination, PaginatedResult } from './../_models/pagination';
import { Component, OnInit } from '@angular/core';
import { DeviceAttributeType } from '../_models/DeviceAttributeType';
import { DeviceAttributeTypeService } from '../_services/deviceattributetype.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-deviceattributetype-list',
  templateUrl: './deviceattributetype-list.component.html',
  styleUrls: ['./deviceattributetype-list.component.css']
})
export class DeviceattributetypeListComponent implements OnInit {

  list: DeviceAttributeType[];
  pagination: Pagination;
  filter = '';
  status = 'Active';
  elementLabel = 'device attribute type';
  pageLabel = 'Device Attribute Types';
  pageRoute = 'deviceattributetypes';

  constructor(private service: DeviceAttributeTypeService, private alertify: AlertifyService
              , private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.list = data['deviceattributetypes'].result;
      this.pagination = data['deviceattributetypes'].pagination;
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
    this.service.getDeviceAttributeTypes(this.pagination.currentPage, this.pagination.itemsPerPage,
        this.filter, activeStatus)
        .subscribe((res: PaginatedResult<DeviceAttributeType[]>) => {
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
      this.service.deactivateDeviceAttributeType(id)
        .subscribe(() => {
          this.loadList();
        }, error => {
          this.alertify.error('Failed to delete ' + this.elementLabel);
        });
    });
  }

}
