import { PaginatedResult } from './../_models/pagination';
import { Observable } from 'rxjs';
import { ProductCapacity } from './../_models/productcapacity';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ProductCapacityService {

  baseUrl = environment.apiUrl;
  controllerPath = 'productcapacity';

  constructor(private http: HttpClient) { }

  // tslint:disable-next-line:max-line-length
  getProductCapacities(page?: number, itemsPerPage?: number, filter?: any, status?: number): Observable<PaginatedResult<ProductCapacity []>> {
    const paginatedResult: PaginatedResult<ProductCapacity []> = new PaginatedResult<ProductCapacity []>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('page', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    if (status != null) {
      params = params.append('active', status.toString());
    }

    if (filter != null) {
      params = params.append('name', filter.name);
      if (filter.productTypeId > 0) {
        params = params.append('productTypeId', filter.productTypeId);
      }
      if (filter.productModelId > 0) {
        params = params.append('productModelId', filter.productModelId);
      }
      if (filter.productManufacturerId > 0) {
        params = params.append('productManufacturerId', filter.productManufacturerId);
      }
    }

    return this.http.get<ProductCapacity[]>(this.baseUrl + this.controllerPath, { observe: 'response', params})
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

  getProductCapacity(id) {
    return this.http.get(this.baseUrl + this.controllerPath + '/' + id);
  }

  updateProductCapacity(element: ProductCapacity) {
    return this.http.put(this.baseUrl + this.controllerPath + '/' + element.id, element);
  }

  deativateProductCapacity(id: number) {
    return this.http.post(this.baseUrl + this.controllerPath + '/' + id + '/deactivate', {});
  }

  addProductCapacity(element: ProductCapacity) {
    return this.http.post(this.baseUrl + this.controllerPath + '/', element);
  }
}
