import { catchError } from 'rxjs/operators';
import { Router, ActivatedRouteSnapshot, Resolve } from '@angular/router';
import { DeviceService } from './../_services/device.service';
import { Injectable } from '@angular/core';
import { AlertifyService } from '../_services/alertify.service';
import { Device } from '../_models/device';
import { Observable, of } from 'rxjs';


@Injectable()
export class DeviceEditResolver implements Resolve<Device> {
    constructor(private service: DeviceService,
        private router: Router,
        private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Device> {
        return this.service.getDevice(route.params['id']).pipe(
            catchError(() => {
                this.alertify.error('Problem retrieving data');
                this.router.navigate(['/devices']);
                return of(null);
            })
        );
    }
}
