import { Router, ActivatedRoute } from '@angular/router';
import { AlertifyService } from './../_services/alertify.service';
import { ProductmodelService } from './../_services/productmodel.service';
import { Pagination, PaginatedResult } from './../_models/pagination';
import { ProductModel } from './../_models/productmodel';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-productmodel-list',
  templateUrl: './productmodel-list.component.html',
  styleUrls: ['./productmodel-list.component.css']
})
export class ProductmodelListComponent implements OnInit {

  list: ProductModel[];
  pagination: Pagination;
  filter = '';
  userParams: any = {};
  status = 'Active';
  elementLabel = 'product model';
  pageLabel = 'Product Models';
  pageRoute = 'productmodels';
  sub;
  qManufacturerId = 0;
  qProductTypeId = 0;

  constructor(private service: ProductmodelService, private alertify: AlertifyService
                , private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.list = data['productmodels'].result;
      this.pagination = data['productmodels'].pagination;
      // this.filter = null;
      this.sub = this.route.queryParams.subscribe(params => {
        this.qManufacturerId = params['manufacturerId'] || 0;
        this.qProductTypeId = params['productTypeId'] || 0;
        console.log('productModelList - ' + this.qManufacturerId.toString());
      });
    });

    this.userParams.name = '';
    this.userParams.productTypeId = this.qProductTypeId;
    this.userParams.productManufacturerId = this.qManufacturerId;
  }

  loadList() {
    let activeStatus = 2;
    if (this.status === 'Active') {
      activeStatus = 1;
    }
    if (this.status === 'Inactive') {
      activeStatus = 0;
    }
    this.service.getProductModels(this.pagination.currentPage, this.pagination.itemsPerPage,
      this.userParams, activeStatus)
      .subscribe((res: PaginatedResult<ProductModel[]>) => {
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
      this.service.deativateProductModel(id)
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

  clearFilter() {
    this.userParams.name = '';
    this.userParams.productTypeId = 0;
    this.userParams.productManufacturerId = 0;
    this.filterTable();
  }

}
