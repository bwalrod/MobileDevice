import { PaginatedResult } from './../../_models/pagination';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from './../../_services/alertify.service';
import { ProductmodelService } from './../../_services/productmodel.service';
import { Component, OnInit, Input, EventEmitter, Output, SimpleChanges } from '@angular/core';
import { ProductModel } from '../../_models/productmodel';

@Component({
  selector: 'app-productmodel-select',
  templateUrl: './productmodel-select.component.html',
  styleUrls: ['./productmodel-select.component.css']
})
export class ProductmodelSelectComponent implements OnInit {
  @Input() selectedModelId: number;
  @Input() selectedProductTypeId: number;
  @Input() selectedManufactureId: number;
  @Input() showNoValue: boolean;
  @Input() noValueLabel = '.: Any :.';
  @Output() selectedOption = new EventEmitter();

  models: ProductModel[];
  filter: any = {};

  constructor(private service: ProductmodelService, private alertify: AlertifyService, private route: ActivatedRoute) { }

  // tslint:disable-next-line:use-life-cycle-interface
  ngOnChanges(changes: SimpleChanges) {
    if (changes['selectedProductTypeId'] || changes['selectedManufactureId']) {
      this.filter.productTypeId = this.selectedProductTypeId;
      this.filter.productManufacturerId = this.selectedManufactureId;
      this.loadList();
    }
  }

  ngOnInit() {
    this.filter.name = '';
    this.filter.productTypeId = this.selectedProductTypeId;
    this.filter.productManufacturerId = this.selectedManufactureId;
    this.loadList();
  }

  loadList() {
    this.service.getProductModels(1, 100, this.filter, 1)
      .subscribe((res: PaginatedResult<ProductModel[]>) => {
        this.models = res.result;
      });
  }

  optionSelected(val: number) {
    this.selectedModelId = val;
    this.selectedOption.emit(this.selectedModelId);
    console.log(this.selectedModelId);
  }

}
