import { catchError } from 'rxjs/operators';
import { AlertifyService } from './../_services/alertify.service';
import { DepartmentService } from './../_services/department.service';
import { Injectable } from '@angular/core';
import { Router, ActivatedRouteSnapshot, Resolve } from '@angular/router';
import { Observable, of } from 'rxjs';
import { Department } from '../_models/department';

@Injectable()
export class DepartmentListResolver implements Resolve<Department[]> {
    pageNumber = 1;
    pageSize = 5;
    status = 1;

    constructor(private departmentService: DepartmentService, private route: Router, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Department[]> {
        const filter = route.queryParams['name'] || '';
        return this.departmentService.getDepartments(this.pageNumber, this.pageSize, filter , this.status).pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data');
                this.route.navigate(['/home']);
                return of(null);
            })
        );
    }
}
