import { DeviceStatusService } from './../_services/devicestatus.service';
import { DeviceStatus } from './../_models/DeviceStatus';
import { catchError } from 'rxjs/operators';
import { AlertifyService } from './../_services/alertify.service';
import { Injectable } from '@angular/core';
import { Router, ActivatedRouteSnapshot, Resolve } from '@angular/router';
import { Observable, of } from 'rxjs';

@Injectable()
export class DeviceStatusListResolver implements Resolve<DeviceStatus[]> {
    pageNumber = 1;
    pageSize = 5;
    status = 1;

    constructor(private service: DeviceStatusService, private route: Router, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<DeviceStatus[]> {
        return this.service.getDeviceStatuses(this.pageNumber, this.pageSize, null , this.status).pipe(
            catchError(() => {
                this.alertify.error('Problem retrieving data');
                this.route.navigate(['/home']);
                return of(null);
            })
        );
    }
}
