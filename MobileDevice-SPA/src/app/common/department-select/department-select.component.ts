import { Department } from './../../_models/department';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { DepartmentService } from 'src/app/_services/department.service';
import { PaginatedResult } from 'src/app/_models/pagination';

@Component({
  selector: 'app-department-select',
  templateUrl: './department-select.component.html',
  styleUrls: ['./department-select.component.css']
})
export class DepartmentSelectComponent implements OnInit {
  @Input() selectedDepartment: number;
  @Input() showNoValue: boolean;
  @Input() noValueLabel = '.: Any :.';
  @Output() selectOption = new EventEmitter();

  departments: Department[];
  filter = '';

  constructor(private service: DepartmentService) { }

  ngOnInit() {
    this.loadList();
  }

  loadList() {
    this.service.getDepartments(1, 100, this.filter, 1)
      .subscribe((res: PaginatedResult<Department[]>) => {
        this.departments = res.result;
      });
  }

  optionSelected(val: number) {
    this.selectedDepartment = val;
    this.selectOption.emit(this.selectedDepartment);
    console.log('Department: ' + this.selectedDepartment.toString());
  }
}
