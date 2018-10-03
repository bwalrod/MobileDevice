import { ProductTypeService } from './../_services/producttype.service';
import { catchError } from 'rxjs/operators';
import { AlertifyService } from '../_services/alertify.service';
import { Injectable } from '@angular/core';
import { Router, ActivatedRouteSnapshot, Resolve } from '@angular/router';
import { Observable, of } from 'rxjs';
import { ProductType } from '../_models/ProductType';

@Injectable()
export class ProductTypeListResolver implements Resolve<ProductType[]> {
    pageNumber = 1;
    pageSize = 5;
    status = 1;

    constructor(private productTypeService: ProductTypeService, private route: Router, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<ProductType[]> {
        return this.productTypeService.getProductTypes(this.pageNumber, this.pageSize, null , this.status).pipe(
            catchError(() => {
                this.alertify.error('Problem retrieving data');
                this.route.navigate(['/home']);
                return of(null);
            })
        );
    }
}
