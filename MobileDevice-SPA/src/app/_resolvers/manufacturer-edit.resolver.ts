import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { AlertifyService } from './../_services/alertify.service';
import { Manufacturer } from './../_models/manufacturer';
import { Injectable } from '@angular/core';
import { ManufacturerService } from '../_services/manufacturer.service';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';

@Injectable()
export class ManufacturerEditResolver implements Resolve<Manufacturer> {
    constructor(private manuService: ManufacturerService, private router: Router, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Manufacturer> {
        return this.manuService.getManufacturer(route.params['id']).pipe(
            catchError( error => {
                this.alertify.error('Problem retrieving data');
                this.router.navigate(['/manufacturers']);
                return of(null);
            })
        );
    }
}
