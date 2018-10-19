import { AlertifyService } from './../_services/alertify.service';
import { AssigneeService } from './../_services/assignee.service';
import { Assignee } from './../_models/assignee';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-assignee-edit',
  templateUrl: './assignee-edit.component.html',
  styleUrls: ['./assignee-edit.component.css']
})
export class AssigneeEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;
  element: Assignee;
  newElement: Assignee = {
    id: 0,
    firstName: '',
    lastName: '',
    departmentId: 0,
    departmentName: '',
    active: true,
    assignmentCount: 0
  };
  elementLabel = 'Assignee';
  elementTypeLabel = 'assignee';
  elementRoute = 'assignees';
  originalElement: Assignee;
  formInvalid = false;

  constructor(private service: AssigneeService, private router: Router,
              private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.element = this.newElement;
    this.originalElement = this.newElement;

    this.route.data.subscribe(data => {
      if (data['assignee']) {
        this.element = data['assignee'];
        this.populateOriginal();
      }
    });
  }

  updateElement() {
    this.service.updateAssignee(this.element)
      .subscribe(next => {
        this.editForm.reset(this.element);
        this.alertify.success(this.elementLabel + ' updated successfully');
      }, error => {
        this.alertify.error(error);
        setTimeout(() => {
          this.alertify.confirm('Would you like to restore the original values?', () => {
            this.restoreOriginal();
          });
        }, 2000);
      });
  }

  insertElement() {
    this.service.addAssignee(this.element)
      .subscribe((element: Assignee) => {
        this.alertify.success(this.elementLabel + ' added successfully');
        this.element = element;
      }, error => {
        this.alertify.error(error);
      });
  }

  submitForm() {
    if (this.editForm.dirty) {
      if (this.haveValuesChanged() || this.element.id === 0) {
        if (this.element.id > 0) {
          this.updateElement();
        } else {
          this.insertElement();
        }
      } else {
        this.alertify.error('You haven\'t changed anything');
        this.editForm.control.markAsPristine();
        this.editForm.control.markAsUntouched();
      }
    }
  }

  deactivateElement(id: number, e: Event) {
    e.stopPropagation();
    e.preventDefault();
    this.alertify.confirm('Are you sure you want to delete this ' + this.elementTypeLabel + '?', () => {
      this.service.deactivateAssignee(id)
        .subscribe(() => {
          this.router.navigate(['/' + this.elementRoute]);
        }, error => {
          this.alertify.error('Failed to delete ' + this.elementTypeLabel);
        });
    });
  }

  returnToList(e: Event) {
    e.stopPropagation();
    e.preventDefault();
    this.router.navigate(['/' + this.elementRoute]);
  }

  resetSelectables() {
    this.element.departmentId = 0;
  }

  setDepartment(department: number) {
    this.element.departmentId = department;
    this.markAsDirty();
    this.testFormValidity();
  }

  markAsDirty() {
    this.editForm.control.markAsDirty();
  }

  isFormValid() {
    return this.element.firstName !== '' &&
      this.element.lastName !== '';
  }

  testFormValidity() {
      this.formInvalid = !this.isFormValid();
  }

  populateOriginal() {
    this.originalElement.id = this.element.id;
    this.originalElement.firstName = this.element.firstName;
    this.originalElement.lastName = this.element.lastName;
    this.originalElement.departmentId = this.element.departmentId;
    this.originalElement.active = this.element.active;
    console.log('populateOriginal');
  }

  haveValuesChanged() {
    return this.originalElement.firstName !== this.element.firstName ||
      this.originalElement.lastName !== this.element.lastName ||
      this.originalElement.departmentId !== this.element.departmentId ||
      this.originalElement.active !== this.element.active;
  }

  restoreOriginal() {
    this.element.firstName = this.originalElement.firstName;
    this.element.lastName = this.originalElement.lastName;
    this.element.departmentId = this.originalElement.departmentId;
    this.element.active = this.originalElement.active;
  }

}
