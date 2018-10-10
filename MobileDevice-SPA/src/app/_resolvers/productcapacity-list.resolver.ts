import { AlertifyService } from './../_services/alertify.service';
import { ProductCapacityService } from './../_services/productcapacity.service';
import { Injectable } from '@angular/core';
import { ProductCapacity } from '../_models/productcapacity';
import { Router, Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ProductcapacityListResolver implements Resolve<ProductCapacity[]> {
    pageNumber = 1;
    pageSize = 5;
    status = 1;

    constructor(private service: ProductCapacityService, private route: Router, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<ProductCapacity[]> {
        return this.service.getProductCapacities(this.pageNumber, this.pageSize, null , this.status).pipe(
            catchError(() => {
                this.alertify.error('Problem retrieving data');
                this.route.navigate(['/home']);
                return of(null);
            })
        );
    }
}

