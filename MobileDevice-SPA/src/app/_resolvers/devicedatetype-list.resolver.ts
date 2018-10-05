import { Observable, of } from 'rxjs';
import { AlertifyService } from './../_services/alertify.service';
import { Injectable } from '@angular/core';
import { DeviceDateTypeService } from '../_services/devicedatetype.service';
import { Router, Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { DeviceDateType } from '../_models/devicedatetype';
import { catchError } from 'rxjs/operators';

@Injectable()
export class DeviceDateTypeListResolver implements Resolve<DeviceDateType[]> {
    pageNumber = 1;
    pageSize = 5;
    status = 1;

    constructor(private service: DeviceDateTypeService, private route: Router,
            private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<DeviceDateType[]> {
        return this.service.getDeviceDateTypes(this.pageNumber, this.pageSize, null, this.status).pipe(
            catchError(() => {
                this.alertify.error('Problem retrieving data');
                this.route.navigate(['/home']);
                return of(null);
            })
        );
    }
}
