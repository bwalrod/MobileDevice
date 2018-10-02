import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { PaginatedResult } from '../_models/pagination';
import { Manufacturer } from '../_models/manufacturer';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ManufacturerService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getManufacturers(page?: number, itemsPerPage?: number, filter?: string, status?: number): Observable<PaginatedResult<Manufacturer []>> {
    const paginatedResult: PaginatedResult<Manufacturer []> = new PaginatedResult<Manufacturer []>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('page', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    if (filter != null) {
      params = params.append('name', filter);
    }

    if (status != null) {
      params = params.append('active', status.toString());
    }

    return this.http.get<Manufacturer[]>(this.baseUrl + 'productmanufacturer', { observe: 'response', params})
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

  getManufacturer(id) {
    return this.http.get(this.baseUrl + 'productmanufacturer/' + id);
  }

  updateManufacturer(manu: Manufacturer) {
    return this.http.put(this.baseUrl + 'productmanufacturer/' + manu.id, manu, {withCredentials: true});
  }

  deactivateManufacturer(id: number) {
    return this.http.post(this.baseUrl + 'productmanufacturer/' + id + '/deactivate', {});
  }

  addManufacturer(manu: Manufacturer) {
    return this.http.post(this.baseUrl + 'productmanufacturer/', manu);
  }

}
