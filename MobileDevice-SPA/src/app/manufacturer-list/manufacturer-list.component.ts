import { Pagination } from './../_models/pagination';
import { Manufacturer } from './../_models/manufacturer';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-manufacturer-list',
  templateUrl: './manufacturer-list.component.html',
  styleUrls: ['./manufacturer-list.component.css']
})
export class ManufacturerListComponent implements OnInit {

  manufacturers: Manufacturer[];
  pagination: Pagination;
  filter = '';
  status = 'Active';

  constructor() { }

  ngOnInit() {
  }

}
