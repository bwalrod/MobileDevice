import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { PaginatedResult } from '../_models/pagination';
import { Manufacturer } from '../_models/manufacturer';

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

  }

}
