import { Assignee } from './../../_models/assignee';
import { AssigneeService } from './../../_services/assignee.service';
import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';

@Component({
  selector: 'app-assignee-ng-select',
  templateUrl: './assignee-ng-select.component.html',
  styleUrls: ['./assignee-ng-select.component.css']
})
export class AssigneeNgSelectComponent implements OnInit {
  @Input() selectedAssigneeId: number;
  @Output() selectOption = new EventEmitter();

  assignees: Assignee[];

  filter = {
    firstName: '',
    lastName: ''
  };

  // selectedAssigneeId: number = null;

  constructor(private service: AssigneeService) {
  }

  ngOnInit() {
    this.loadList();
  }

  loadList() {
    this.service.getAllAssignees(this.filter, 1)
      .subscribe((res: Assignee[]) => {
        this.assignees = res;
      });
  }

  optionSelected(val: number) {
    this.selectedAssigneeId = val;
    this.selectOption.emit(this.selectedAssigneeId);
    console.log(this.selectedAssigneeId);
  }
}
