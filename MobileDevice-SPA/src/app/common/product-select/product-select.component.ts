import { Product } from './../../_models/product';
import { Component, OnInit, EventEmitter, Input, Output, SimpleChange, SimpleChanges } from '@angular/core';
import { ProductService } from 'src/app/_services/product.service';
import { PaginatedResult } from 'src/app/_models/pagination';

@Component({
  selector: 'app-product-select',
  templateUrl: './product-select.component.html',
  styleUrls: ['./product-select.component.css']
})
export class ProductSelectComponent implements OnInit {
  @Input() selectedProductId: number;
  @Input() selectedProductTypeId: number;
  @Input() showNoValue: boolean;
  @Input() noValueLabel = '.: Any :.';
  @Output() selectedOption = new EventEmitter();

  products: Product[];
  filter: any = {};

  constructor(private service: ProductService) { }

  // tslint:disable-next-line:use-life-cycle-interface
  ngOnChanges(changes: SimpleChanges) {
    if (changes['selectedProductTypeId']) {
      this.filter.partNum = '';
      this.filter.productTypeId = this.selectedProductTypeId;
      this.loadList();
    }
  }

  ngOnInit() {
    this.filter.partNum = '';
    this.filter.productTypeId = this.selectedProductTypeId;
    this.loadList();
  }

  loadList() {
    this.service.getAllProducts(this.filter, 1)
      .subscribe((res: Product[]) => {
        this.products = res;
      });
  }

  optionSelected(val: number) {
    this.selectedProductId = val;
    this.selectedOption.emit(this.selectedProductId);
    console.log(this.selectedProductId);
  }

  makeProductValue(p: any) {
    const val = p.productManufacturerName + ' ' + p.productModelName + ' ' + p.productCapacityName + ' | ' + p.partNum;
    return val;
  }
}
