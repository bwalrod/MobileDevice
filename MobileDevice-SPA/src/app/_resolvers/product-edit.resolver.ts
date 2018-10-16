import { catchError } from 'rxjs/operators';
import { Product } from './../_models/product';
import { AlertifyService } from './../_services/alertify.service';
import { Router, Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { ProductService } from './../_services/product.service';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

@Injectable()
export class ProductEditResolver implements Resolve<Product> {
    constructor(private service: ProductService,
        private router: Router,
        private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Product> {
        return this.service.getProduct(route.params['id']).pipe(
            catchError(() => {
                this.alertify.error('Problem retrieving data');
                this.router.navigate(['/products']);
                return of(null);
            })
        );
    }
}
