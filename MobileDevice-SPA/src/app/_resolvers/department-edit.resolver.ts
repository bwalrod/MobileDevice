import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { Department } from './../_models/department';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AlertifyService } from './../_services/alertify.service';
import { DepartmentService } from './../_services/department.service';
import { Injectable } from '@angular/core';


@Injectable()
export class DepartmentEditResolver implements Resolve<Department> {
    constructor(private departmentService: DepartmentService,
        private router: Router,
        private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Department> {
        return this.departmentService.getDepartment(route.params['id']).pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data');
                this.router.navigate(['/departments']);
                return of(null);
            })
        );
    }
}
