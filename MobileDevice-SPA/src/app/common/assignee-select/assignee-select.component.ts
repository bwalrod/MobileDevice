import { AssigneeService } from './../../_services/assignee.service';
import { Assignee } from './../../_models/assignee';
import { Component, OnInit, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-assignee-select',
  templateUrl: './assignee-select.component.html',
  styleUrls: ['./assignee-select.component.css']
})
export class AssigneeSelectComponent implements OnInit {
  @Input() selectedAssignee: number;
  @Input() showNoValue: boolean;
  @Input() noValueLabel = '.: Any :.';
  @Output() selectOption = new EventEmitter();

  assignees: Assignee[];
  filter = {
    firstName: '',
    lastName: ''
  };

  constructor(private service: AssigneeService) { }

  ngOnInit() {
    this.loadList();
  }

  loadList() {
    // this.service.getAssignees(1, 0, this.filter, 1)
    //   .subscribe((res: PaginatedResult<Assignee[]>) => {
    //     this.assignees = res.result;
    //   });
    this.service.getAllAssignees(this.filter, 1)
      .subscribe((res: Assignee[]) => {
        this.assignees = res;
      });
  }

  optionSelected(val: number) {
    this.selectedAssignee = val;
    this.selectOption.emit(this.selectedAssignee);
    console.log(this.selectedAssignee);
  }

}
