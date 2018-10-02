import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { AlertifyService } from './../_services/alertify.service';
import { ManufacturerService } from './../_services/manufacturer.service';
import { Manufacturer } from './../_models/manufacturer';
import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';

@Injectable()
export class ManufacturerListResolver implements Resolve<Manufacturer[]> {
    pageNumber = 1;
    pageSize = 5;
    status = 1;

    constructor(private manuService: ManufacturerService, private route: Router, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Manufacturer[]> {
        return this.manuService.getManufacturers(this.pageNumber, this.pageSize, null, this.status).pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data');
                this.route.navigate(['/home']);
                return of(null);
            })
        );
    }
}
