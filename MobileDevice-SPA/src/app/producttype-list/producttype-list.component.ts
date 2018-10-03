import { Pagination, PaginatedResult } from './../_models/pagination';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductType } from './../_models/ProductType';
import { Component, OnInit } from '@angular/core';
import { ProductTypeService } from '../_services/producttype.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-producttype-list',
  templateUrl: './producttype-list.component.html',
  styleUrls: ['./producttype-list.component.css']
})
export class ProducttypeListComponent implements OnInit {

  list: ProductType[];
  pagination: Pagination;
  filter = '';
  status = 'Active';
  elementLabel = 'product type';
  pageLabel = 'Product Types';
  pageRoute = 'producttypes';

  constructor(private service: ProductTypeService, private alertify: AlertifyService
                , private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.list = data['producttypes'].result;
      this.pagination = data['producttypes'].pagination;
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
    this.service.getProductTypes(this.pagination.currentPage, this.pagination.itemsPerPage,
      this.filter, activeStatus)
      .subscribe((res: PaginatedResult<ProductType[]>) => {
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
      this.service.deactivateProductType(id)
        .subscribe(() => {
          this.loadList();
        }, error => {
          this.alertify.error('Failed to delete ' + this.elementLabel);
        });
    });
  }
}
