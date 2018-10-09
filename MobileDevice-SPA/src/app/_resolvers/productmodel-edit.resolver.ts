import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { ActivatedRouteSnapshot } from '@angular/router';
import { AlertifyService } from './../_services/alertify.service';
import { ProductModel } from './../_models/productmodel';
import { Injectable } from '@angular/core';
import { Resolve, Router } from '@angular/router';
import { ProductmodelService } from '../_services/productmodel.service';


@Injectable()
export class ProductModelEditResolver implements Resolve<ProductModel> {
    constructor(private service: ProductmodelService,
        private router: Router,
        private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<ProductModel> {
        return this.service.getProductModel(route.params['id']).pipe(
            catchError(() => {
                this.alertify.error('Problem retrieving data');
                this.router.navigate(['/productmodels']);
                return of(null);
            })
        );
    }
}
