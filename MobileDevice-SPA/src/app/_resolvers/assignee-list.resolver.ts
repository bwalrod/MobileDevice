import { AlertifyService } from './../_services/alertify.service';
import { Assignee } from './../_models/assignee';
import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AssigneeService } from '../_services/assignee.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class AssigneeListResolver implements Resolve<Assignee[]> {
    pageNumber = 1;
    pageSize = 10;
    status = 1;

    constructor(private service: AssigneeService, private route: Router, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Assignee[]> {
        console.log('resolve - assignee-list');
        console.log(route.queryParams['departmentId']);
        const filter = {
            firstName: route.queryParams['firstName'] || '',
            lastName: route.queryParams['lastName'] || '',
            departmentId: route.queryParams['departmentId'] || 0 };
        console.log(filter);
        return this.service.getAssignees(this.pageNumber, this.pageSize, filter, this.status).pipe(
            catchError(() => {
                this.alertify.error('Problem retrieving data');
                this.route.navigate(['/home']);
                return of(null);
            })
        );
    }
}
