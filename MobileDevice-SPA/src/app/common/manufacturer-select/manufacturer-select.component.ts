import { PaginatedResult } from './../../_models/pagination';
import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { Manufacturer } from '../../_models/manufacturer';
import { ManufacturerService } from '../../_services/manufacturer.service';
import { AlertifyService } from '../../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-manufacturer-select',
  templateUrl: './manufacturer-select.component.html',
  styleUrls: ['./manufacturer-select.component.css']
})
export class ManufacturerSelectComponent implements OnInit {
  @Input() selectedManufacturer: number;
  @Input() showNoValue: boolean;
  @Input() noValueLabel = '.: Any :.';
  @Output() selectOption = new EventEmitter();

  manufacturers: Manufacturer[];
  filter = '';

  constructor(private service: ManufacturerService, private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.loadList();
  }

  loadList() {
    this.service.getManufacturers(1, 100, this.filter, 1)
      .subscribe((res: PaginatedResult<Manufacturer[]>) => {
        this.manufacturers = res.result;
      });
  }

  optionSelected(val: number) {
    this.selectedManufacturer = val;
    this.selectOption.emit(this.selectedManufacturer);
    console.log(this.selectedManufacturer);
  }
}

