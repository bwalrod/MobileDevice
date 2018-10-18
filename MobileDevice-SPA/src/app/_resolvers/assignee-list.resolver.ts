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
        return this.service.getAssignees(this.pageNumber, this.pageSize, null, this.status).pipe(
            catchError(() => {
                this.alertify.error('Problem retrieving data');
                this.route.navigate(['/home']);
                return of(null);
            })
        );
    }
}
