import { PaginatedResult } from './../../_models/pagination';
import { ProductCapacityService } from './../../_services/productcapacity.service';
import { Component, OnInit, Input, Output, EventEmitter, SimpleChanges } from '@angular/core';
import { ProductCapacity } from 'src/app/_models/productcapacity';

@Component({
  selector: 'app-productcapacity-select',
  templateUrl: './productcapacity-select.component.html',
  styleUrls: ['./productcapacity-select.component.css']
})
export class ProductcapacitySelectComponent implements OnInit {
  @Input() selectedCapacityId: number;
  @Input() selectedCapacityName: string;
  @Input() selectedProductTypeId: number;
  @Input() selectedManufacturerId: number;
  @Input() selectedModelId: number;
  @Input() showNoValue: boolean;
  @Input() noValueLabel = '.: Any :.';
  @Input() showDistinctValues = false;
  @Output() selectedOption = new EventEmitter();

  capacities: ProductCapacity[];
  capacityNames: string[];
  filter: any = {};

  constructor(private service: ProductCapacityService) { }

  // tslint:disable-next-line:use-life-cycle-interface
  ngOnChanges(changes: SimpleChanges) {
    if (changes['selectedProductTypeId'] || changes['selectedManufacturerId'] || changes['selectedModelId']) {
      this.filter.name = '';
      this.filter.productTypeId = this.selectedProductTypeId;
      this.filter.productManufacturerId = this.selectedManufacturerId;
      this.filter.productModelId = this.selectedModelId;
      console.log('capacitiesChanges' + JSON.stringify(this.filter));
      this.loadlist();
    }
  }

  ngOnInit() {
    this.filter.name = '';
    this.filter.productTypeId = this.selectedProductTypeId;
    this.filter.productManufacturerId = this.selectedManufacturerId;
    this.filter.productModelId = this.selectedModelId;
    this.loadlist();
  }

  loadlist() {
    this.service.getProductCapacities(1, 100, this.filter, 1)
      .subscribe((res: PaginatedResult<ProductCapacity[]>) => {
          this.capacities = res.result;
          this.capacityNames = this.getDistinct(this.capacities);
          console.log('capacities.length: ' + this.capacities.length);
          console.log('capacityNames.length: ' + this.capacityNames.length);
      });
  }

  getDistinct(array: ProductCapacity[]) {
    const unique = {};
    const distinct = [];
    array.forEach(function (x) {
      if (!unique[x.name]) {
        distinct.push(x.name);
        unique[x.name] = true;
      }
    });
    return distinct;
  }


  optionSelected(val: number) {
    this.selectedCapacityId = val;
    this.selectedOption.emit(this.selectedCapacityId);
    console.log(this.selectedCapacityId);
  }

  optionSelectedValue(val: string) {
    this.selectedCapacityName = val;
    this.selectedOption.emit(this.selectedCapacityName);
    console.log('Product Capacity Select Name: ' + this.selectedCapacityName);
  }
}
