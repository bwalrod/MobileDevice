import { catchError } from 'rxjs/operators';
import { AlertifyService } from './../_services/alertify.service';
import { DevicedateService } from './../_services/devicedate.service';
import { Injectable } from '@angular/core';
import { Router, ActivatedRouteSnapshot, Resolve } from '@angular/router';
import { Observable, of } from 'rxjs';
import { DeviceDate } from '../_models/devicedate';

@Injectable()
export class DeviceDateListResolver implements Resolve<DeviceDate[]> {
    pageNumber = 1;
    pageSize = 5;
    status = 1;

    constructor(private service: DevicedateService, private route: Router,
            private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<DeviceDate[]> {
        return this.service.getDeviceDates(this.pageNumber, this.pageSize, null, this.status).pipe(
            catchError(() => {
                this.alertify.error('Problem retrieving data');
                this.route.navigate(['/home']);
                return of(null);
            })
        );
    }
}
