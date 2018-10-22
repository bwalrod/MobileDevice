import { Observable, of } from 'rxjs';
import { AlertifyService } from './../_services/alertify.service';
import { DeviceService } from './../_services/device.service';
import { Device } from './../_models/device';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Injectable } from '@angular/core';
import { catchError } from 'rxjs/operators';


@Injectable()
export class DeviceListResolver implements Resolve<Device[]> {
    pageNumber = 1;
    pageSize = 10;
    status = 1;

    constructor(private service: DeviceService, private route: Router, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Device[]> {
        const filter = {
            serialNumber: route.queryParams['serialNumber'] || '',
            esn: route.queryParams['esn'] || '',
            os: route.queryParams['os'] || '',
            productId: route.queryParams['productId'] || '',
            productModelId: route.queryParams['productModelId'] || '',
            assigneeDepartmentId: route.queryParams['assigneeDepartmentId'] || '',
            productCapacityId: route.queryParams['productCapacityId'] || '',
            assigneeId: route.queryParams['assigneeId'] || ''
        };
        return this.service.getDevices(this.pageNumber, this.pageSize, filter , this.status).pipe(
            catchError(() => {
                this.alertify.error('Problem retrieving data');
                this.route.navigate(['/home']);
                return of(null);
            })
        );
    }
}
