import { AssigneeService } from './../../_services/assignee.service';
import { Assignee } from './../../_models/assignee';
import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { mergeMap } from 'rxjs/operators';
import { TypeaheadMatch } from 'ngx-bootstrap/typeahead';

@Component({
  selector: 'app-assignee-typeahead',
  templateUrl: './assignee-typeahead.component.html',
  styleUrls: ['./assignee-typeahead.component.css']
})
export class AssigneeTypeaheadComponent implements OnInit {
  asyncSelected: string;
  typeaheadLoading: boolean;
  typeaheadNoResults: boolean;
  dataSource: Observable<any>;
  assignees: Assignee[] = [
    { id: 0,
      firstName: 'Bob',
      lastName: 'Builder',
      departmentId: 0,
      departmentName: '',
      active: true,
      assignmentCount: 0,
      fullNameLF: ''
    }
  ];
  filter = {
    firstName: '',
    lastName: ''
  };
  selectedAssigneeId: number = null;

  constructor(private service: AssigneeService) {
    this.dataSource = Observable.create((observer: any) => {
      // Runs on every search
      observer.next(this.asyncSelected);
    })
      .pipe(
        mergeMap((token: string) => this.getStatesAsObservable(token))
      );
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

  getStatesAsObservable(token: string): Observable<any> {
    const query = new RegExp(token, 'ig');

    return of(
      this.assignees.filter((state: any) => {
        return query.test(state.fullNameLF);
      })
    );
  }

  changeTypeaheadLoading(e: boolean): void {
    this.typeaheadLoading = e;
  }

  typeaheadOnSelect(e: TypeaheadMatch): void {
    console.log('Selected value: ', e.value);
  }

}
