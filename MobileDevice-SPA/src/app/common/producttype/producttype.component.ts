import { PaginatedResult } from './../../_models/pagination';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from './../../_services/alertify.service';
import { ProductType } from './../../_models/ProductType';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ProductTypeService } from '../../_services/producttype.service';

@Component({
  selector: 'app-producttype',
  templateUrl: './producttype.component.html',
  styleUrls: ['./producttype.component.css']
})
export class ProducttypeComponent implements OnInit {
  @Output() selectOption = new EventEmitter();

  list: ProductType[];
  filter = '';
  selectedProductType: number;

  constructor(private service: ProductTypeService, private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    // this.route.data.subscribe(data => {
    //   this.list = data['producttypes'].result;
    // });
    this.loadList();
  }

  loadList() {
    this.service.getProductTypes(1, 100, this.filter, 1)
      .subscribe((res: PaginatedResult<ProductType[]>) => {
        this.list = res.result;
      });
  }

  optionSelected(val: number) {
    this.selectedProductType = val;
    this.selectOption.emit(this.selectedProductType);
    console.log(this.selectedProductType);
  }

}
