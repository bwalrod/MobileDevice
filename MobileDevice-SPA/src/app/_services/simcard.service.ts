import { Simcard } from './../_models/simcard';
import { PaginatedResult } from './../_models/pagination';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class SimcardService {
  baseUrl = environment.apiUrl;
  route = 'simcard';

  constructor(private http: HttpClient) { }

  getSimcards(page?: number, itemsPerPage?: number, filter?: any, status?: number): Observable<PaginatedResult<Simcard []>> {
    const paginatedResult: PaginatedResult<Simcard []> = new PaginatedResult<Simcard []>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('page', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    if (filter != null) {
      params = params.append('iccid', filter.iccid);
      params = params.append('phoneNumber', filter.phoneNumber);
      params = params.append('carrier', filter.carrier);
    }

    if (status != null) {
      params = params.append('active', status.toString());
    }

    return this.http.get<Simcard[]>(this.baseUrl + this.route, { observe: 'response', params})
    .pipe(
      map(response => {
        paginatedResult.result = response.body;
        if (response.headers.get('Pagination') != null) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return paginatedResult;
      })
    );
  }

  getSimcard(id) {
    return this.http.get(this.baseUrl + this.route + '/' + id);
  }

  updateSimcard(simcard: Simcard) {
    return this.http.put(this.baseUrl + this.route + '/' + simcard.id, simcard);
  }

  deactivateSimcard(id: number) {
    return this.http.post(this.baseUrl + this.route + '/' + id + '/deactivate', {});
  }

  addSimcard(simcard: Simcard) {
    return this.http.post(this.baseUrl + this.route + '/', simcard);
  }
}
