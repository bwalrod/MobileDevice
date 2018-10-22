import { AlertifyService } from './../_services/alertify.service';
import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { ProductModel } from '../_models/productmodel';
import { ProductmodelService } from '../_services/productmodel.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ProductModelListResolver implements Resolve<ProductModel[]> {
    pageNumber = 1;
    pageSize = 5;
    status = 1;

    constructor(private productTypeService: ProductmodelService, private route: Router, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<ProductModel[]> {
        const filter = {
            name: route.queryParams['name'] || '',
            productManufacturerId: route.queryParams['manufacturerId'] || '',
            productTypeId: route.queryParams['productTypeId'] || ''
        };
        return this.productTypeService.getProductModels(this.pageNumber, this.pageSize, filter , this.status).pipe(
            catchError(() => {
                this.alertify.error('Problem retrieving data');
                this.route.navigate(['/home']);
                return of(null);
            })
        );
    }
}
