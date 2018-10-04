import { DeviceAttributeTypeService } from './../_services/deviceattributetype.service';
import { DeviceAttributeType } from './../_models/deviceattributetype';
import { catchError } from 'rxjs/operators';
import { AlertifyService } from '../_services/alertify.service';
import { Injectable } from '@angular/core';
import { Router, ActivatedRouteSnapshot, Resolve } from '@angular/router';
import { Observable, of } from 'rxjs';

@Injectable()
export class DeviceAttributeTypeListResolver implements Resolve<DeviceAttributeType[]> {
    pageNumber = 1;
    pageSize = 5;
    status = 1;

    constructor(private service: DeviceAttributeTypeService, private route: Router, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<DeviceAttributeType[]> {
        return this.service.getDeviceAttributeTypes(this.pageNumber, this.pageSize, null , this.status).pipe(
            catchError(() => {
                this.alertify.error('Problem retrieving data');
                this.route.navigate(['/home']);
                return of(null);
            })
        );
    }
}
