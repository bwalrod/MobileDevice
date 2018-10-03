import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { Injectable } from '@angular/core';
import { ProductType } from '../_models/ProductType';
import { ProductTypeService } from '../_services/producttype.service';


@Injectable()
export class ProductTypeEditResolver implements Resolve<ProductType> {
    constructor(private productTypeService: ProductTypeService,
        private router: Router,
        private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<ProductType> {
        return this.productTypeService.getProductType(route.params['id']).pipe(
            catchError(() => {
                this.alertify.error('Problem retrieving data');
                this.router.navigate(['/producttypes']);
                return of(null);
            })
        );
    }
}
