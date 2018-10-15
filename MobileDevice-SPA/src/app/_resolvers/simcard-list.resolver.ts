import { Observable, of } from 'rxjs';
import { AlertifyService } from './../_services/alertify.service';
import { Router, ActivatedRouteSnapshot, Resolve } from '@angular/router';
import { SimcardService } from './../_services/simcard.service';
import { Injectable } from '@angular/core';
import { Simcard } from '../_models/simcard';
import { catchError } from 'rxjs/operators';

@Injectable()
export class SimcardListResolver implements Resolve<Simcard[]> {
    pageNumber = 1;
    pageSize = 10;
    status = 1;

    constructor(private service: SimcardService, private route: Router, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Simcard[]> {
        return this.service.getSimcards(this.pageNumber, this.pageSize, null , this.status).pipe(
            catchError(() => {
                this.alertify.error('Problem retrieving data');
                this.route.navigate(['/home']);
                return of(null);
            })
        );
    }
}
