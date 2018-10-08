import { DeviceDateType } from '../_models/devicedatetype';
import { DeviceDateTypeService } from '../_services/devicedatetype.service';
import { Router, ActivatedRouteSnapshot, Resolve } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';

@Injectable()
export class DeviceDateTypeEditResolver implements Resolve<DeviceDateType> {
    constructor(private service: DeviceDateTypeService,
        private router: Router,
        private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<DeviceDateType> {
        return this.service.getDeviceDateType(route.params['id']).pipe(
            catchError(() => {
                this.alertify.error('Problem retrieving data');
                this.router.navigate(['/producttypes']);
                return of(null);
            })
        );
    }
}
