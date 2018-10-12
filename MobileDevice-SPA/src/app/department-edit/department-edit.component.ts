import { ActivatedRoute, Router } from '@angular/router';
import { DepartmentService } from './../_services/department.service';
import { Department } from './../_models/department';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-department-edit',
  templateUrl: './department-edit.component.html',
  styleUrls: ['./department-edit.component.css']
})
export class DepartmentEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;
  department: Department;
  newDepartment: Department = {
    id: 0,
    name: '',
    active: true,
    assigneeCount: 0
  };
  originalDepartment: Department;

  constructor(private deptService: DepartmentService, private router: Router,
                private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.department = this.newDepartment;
    this.originalDepartment = this.newDepartment;

    this.route.data.subscribe(data => {
      if (data['department']) {
        this.department = data['department'];
        this.originalDepartment.name = this.department.name;
        this.originalDepartment.id = this.department.id;
        this.originalDepartment.active = this.department.active;
        this.originalDepartment.assigneeCount = 0;
      }
    });
  }

  submitForm() {
    if (this.editForm.dirty && this.editForm.valid) {
      if (this.department.id > 0) {
        this.updateDepartment();
      } else {
        this.insertDepartment();
      }
    }
  }

  updateDepartment() {
    // const iActive = this.editForm.controls['active'].value === true ? 1 : 0;
    // this.department.active = iActive;
    this.deptService.updateDepartment(this.department)
      .subscribe(next => {
        this.editForm.reset(this.department);
        this.alertify.success('Department updated successfully');
      }, error => {
        this.alertify.error(error);
        this.editForm.reset(this.originalDepartment);
      });
  }

  insertDepartment() {
    this.deptService.addDepartment(this.department)
      .subscribe((department: Department) => {
        this.alertify.success('Department added successfully');
        this.department = department;
        // this.editForm.reset(this.department);
        // this.router.navigate(['/departments/edit/' + this.department.id]);
      }, error => {
        this.alertify.error(error);
        this.editForm.reset(this.originalDepartment);
      });
  }

  deactivateDepartment(id: number, e: Event) {
    this.alertify.confirm('Are you sure you want to delete this department?', () => {
      this.deptService.deactivateDepartment(id)
      .subscribe(() => {
        e.stopPropagation();
        e.preventDefault();
        this.router.navigate(['/departments']);
      }, error => {
        this.alertify.error('Failed to delete department');
      });
    });
  }

  returnToList(e: Event) {
    e.stopPropagation();
    e.preventDefault();
    this.router.navigate(['/departments']);
  }

}
