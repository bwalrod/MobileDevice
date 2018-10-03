import { ProductType } from './../_models/ProductType';
import { PaginatedResult } from './../_models/pagination';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductTypeService {

  baseUrl = environment.apiUrl;
  controllerPath = 'producttype';

  constructor(private http: HttpClient) { }

  getProductTypes(page?: number, itemsPerPage?: number, filter?: string, status?: number): Observable<PaginatedResult<ProductType []>> {
    const paginatedResult: PaginatedResult<ProductType []> = new PaginatedResult<ProductType []>();

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

    return this.http.get<ProductType[]>(this.baseUrl + this.controllerPath, { observe: 'response', params})
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

  getProductType(id) {
    return this.http.get(this.baseUrl + this.controllerPath + '/' + id);
  }

  updateProductType(productType: ProductType) {
    return this.http.put(this.baseUrl + this.controllerPath + '/' + productType.id, productType);
  }

  deactivateProductType(id: number) {
    return this.http.post(this.baseUrl + this.controllerPath + '/' + id + '/deactivate', {});
  }

  addProductType(productType: ProductType) {
    return this.http.post(this.baseUrl + this.controllerPath + '/', productType);
  }

}
