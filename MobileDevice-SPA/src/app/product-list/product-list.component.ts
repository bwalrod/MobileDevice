import { PaginatedResult } from './../_models/pagination';
import { Router, ActivatedRoute } from '@angular/router';
import { Product } from './../_models/product';
import { Component, OnInit } from '@angular/core';
import { Pagination } from '../_models/pagination';
import { ProductService } from '../_services/product.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {

  list: Product[];
  pagination: Pagination;
  filter: any = {};
  status = 'Active';
  elementLabel = 'product';
  pageLabel = 'Products';
  pageRoute = 'products';

  constructor(private service: ProductService, private alertify: AlertifyService,
                private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.list = data['products'].result;
      this.pagination = data['products'].pagination;
    });

    this.filter.partNum = '';
    this.filter.productTypeId = 0;
    this.filter.productModelId = 0;
    this.filter.productManufacturerId = 0;
    this.filter.productCapacityId = 0;
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
    this.service.getProducts(this.pagination.currentPage, this.pagination.itemsPerPage,
      this.filter, activeStatus)
      .subscribe((res: PaginatedResult<Product[]>) => {
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
      this.service.deativateProduct(id)
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

  filterByCapacity(capacityFilter: number) {
    this.filter.productCapacityId = capacityFilter;
    this.filterTable();
  }

  clearFilter() {
    this.filter.partNum = '';
    this.filter.productTypeId = 0;
    this.filter.productManufacturerId = 0;
    this.filter.productModelId = 0;
    this.filter.productCapacityId = 0;
    this.filterTable();
  }
}
