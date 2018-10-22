import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { AlertifyService } from './../_services/alertify.service';
import { Router, ActivatedRouteSnapshot, Resolve } from '@angular/router';
import { ProductService } from './../_services/product.service';
import { Product } from './../_models/product';
import { Injectable } from '@angular/core';


@Injectable()
export class ProductListResolver implements Resolve<Product[]> {
    pageNumber = 1;
    pageSize = 5;
    status = 1;

    constructor(private service: ProductService, private route: Router, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Product[]> {
        const filter = {
            partNum: '',
            productModelId: route.queryParams['productModelId'] || '',
            productCapacityId: route.queryParams['productCapacityId'] || ''
        };
        return this.service.getProducts(this.pageNumber, this.pageSize, filter, this.status).pipe(
            catchError(() => {
                this.alertify.error('Problem retrieving data');
                this.route.navigate(['/home']);
                return of(null);
            })
        );
    }
}
