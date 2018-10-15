import { Observable, of } from 'rxjs';
import { AlertifyService } from './../_services/alertify.service';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Simcard } from './../_models/simcard';
import { Injectable } from '@angular/core';
import { SimcardService } from '../_services/simcard.service';
import { catchError } from 'rxjs/operators';

@Injectable()
export class SimcardEditResolver implements Resolve<Simcard> {
    constructor(private service: SimcardService,
        private router: Router,
        private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Simcard> {
        return this.service.getSimcard(route.params['id']).pipe(
            catchError(() => {
                this.alertify.error('Problem retrieving data');
                this.router.navigate(['/productcapacities']);
                return of(null);
            })
        );
    }
}
