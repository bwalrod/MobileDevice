import { DeviceAttributeType } from './../_models/deviceattributetype';
import { DeviceAttributeTypeService } from './../_services/deviceattributetype.service';
import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { Injectable } from '@angular/core';


@Injectable()
export class DeviceAttributeTypeEditResolver implements Resolve<DeviceAttributeType> {
    constructor(private service: DeviceAttributeTypeService,
        private router: Router,
        private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<DeviceAttributeType> {
        return this.service.getDeviceAttributeTypes(route.params['id']).pipe(
            catchError(() => {
                this.alertify.error('Problem retrieving data');
                this.router.navigate(['/devicestatuses']);
                return of(null);
            })
        );
    }
}
