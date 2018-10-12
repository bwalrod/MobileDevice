import { ProductCapacityService } from './../_services/productcapacity.service';
import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { ProductCapacity } from '../_models/productcapacity';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ProductCapacityEditResolver implements Resolve<ProductCapacity> {
    constructor(private service: ProductCapacityService,
        private router: Router,
        private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<ProductCapacity> {
        return this.service.getProductCapacity(route.params['id']).pipe(
            catchError(() => {
                this.alertify.error('Problem retrieving data');
                this.router.navigate(['/productcapacities']);
                return of(null);
            })
        );
    }
}
