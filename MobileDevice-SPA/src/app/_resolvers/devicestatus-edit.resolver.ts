import { DeviceStatusService } from './../_services/devicestatus.service';
import { DeviceStatus } from './../_models/DeviceStatus';
import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AlertifyService } from './../_services/alertify.service';
import { Injectable } from '@angular/core';


@Injectable()
export class DeviceStatusEditResolver implements Resolve<DeviceStatus> {
    constructor(private service: DeviceStatusService,
        private router: Router,
        private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<DeviceStatus> {
        return this.service.getDeviceStatus(route.params['id']).pipe(
            catchError(() => {
                this.alertify.error('Problem retrieving data');
                this.router.navigate(['/devicestatuses']);
                return of(null);
            })
        );
    }
}
