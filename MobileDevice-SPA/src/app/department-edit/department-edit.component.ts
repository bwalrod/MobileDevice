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

  constructor(private deptService: DepartmentService, private router: Router,
                private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.department = data['department'];
    });
  }

  updateDepartment() {
    if (this.editForm.dirty) {
      this.deptService.updateDepartment(this.department)
        .subscribe(next => {
          this.alertify.success('Department updated successfully');
          this.editForm.reset(this.department);
        }, error => {
          this.alertify.error(error);
        });
    }
  }

  deactivateDepartment(id: number) {
    this.alertify.confirm('Are you sure you want to delete this department?', () => {
      this.deptService.deactivateDepartment(id)
      .subscribe(() => {
        this.router.navigate(['/departments']);
      }, error => {
        this.alertify.error('Failed to delete department');
      });
    });
  }

  returnToList() {
    this.router.navigate(['/departments']);
  }

}
