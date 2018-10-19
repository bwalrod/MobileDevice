import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { AlertifyService } from './../_services/alertify.service';
import { Assignee } from './../_models/assignee';
import { Injectable } from '@angular/core';
import { AssigneeService } from '../_services/assignee.service';
import { Router, ActivatedRouteSnapshot, Resolve } from '@angular/router';

@Injectable()
export class AssigneeEditResolver implements Resolve<Assignee> {
    constructor(private service: AssigneeService,
        private router: Router,
        private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Assignee> {
        return this.service.getAssignee(route.params['id']).pipe(
            catchError(() => {
                this.alertify.error('Problem retrieving data');
                this.router.navigate(['/assignees']);
                return of(null);
            })
        );
    }
}
