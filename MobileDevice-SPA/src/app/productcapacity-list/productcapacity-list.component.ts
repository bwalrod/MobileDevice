import { PaginatedResult } from './../_models/pagination';
import { Router, ActivatedRoute } from '@angular/router';
import { AlertifyService } from './../_services/alertify.service';
import { ProductCapacityService } from './../_services/productcapacity.service';
import { Component, OnInit } from '@angular/core';
import { ProductCapacity } from '../_models/productcapacity';
import { Pagination } from '../_models/pagination';

@Component({
  selector: 'app-productcapacity-list',
  templateUrl: './productcapacity-list.component.html',
  styleUrls: ['./productcapacity-list.component.css']
})
export class ProductcapacityListComponent implements OnInit {

  list: ProductCapacity[];
  pagination: Pagination;
  filter: any = {
    name: '',
    productTypeId: 0,
    productManufacturerId: 0,
    productModelId: 0
  };
  status = 'Active';
  elementLabel = 'product capacity';
  pageLabel = 'Product Capacities';
  pageRoute = 'productcapacities';

  constructor(private service: ProductCapacityService, private alertify: AlertifyService
                , private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.list = data['productcapacities'].result;
      this.pagination = data['productcapacities'].pagination;
    });

    this.filter.name = '';
    this.filter.productModelId = 0;
    this.filter.productManufacturerId = 0;
    this.filter.productTypeId = 0;
  }

  loadList() {
    let activeStatus = 2;
    if (this.status === 'Active') {
      activeStatus = 1;
    }
    if (this.status === 'Inactive') {
      activeStatus = 0;
    }
    this.service.getProductCapacities(this.pagination.currentPage, this.pagination.itemsPerPage,
        this.filter, activeStatus)
        .subscribe((res: PaginatedResult<ProductCapacity[]>) => {
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
      this.service.deativateProductCapacity(id)
        .subscribe(() => {
          this.loadList();
        }, error => {
          this.alertify.error('Failed to delete ' + this.elementLabel);
        });
    });
  }

  filterByType(typeFilter: number) {
    this.filter.productTypeId = typeFilter;
    this.filterTable();
  }

  filterByManufacturer(manufacturerFilter: number) {
    this.filter.productManufacturerId = manufacturerFilter;
    this.filterTable();
  }

  filterByModel(modelFilter: number) {
    this.filter.productModelId = modelFilter;
    this.filterTable();
  }

  clearFilter() {
    this.filter.name = '';
    this.filter.productTypeId = 0;
    this.filter.productManufacturerId = 0;
    this.filter.productModelId = 0;
    this.filterTable();
  }
}
