import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../_models/pagination';
import { ProductModel } from '../_models/productmodel';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ProductmodelService {

  baseUrl = environment.apiUrl;
  controllerPath = 'productmodel';

  constructor(private http: HttpClient) { }

  getProductModels(page?: number, itemsPerPage?: number, filter?: any, status?: number): Observable<PaginatedResult<ProductModel []>> {
    const paginatedResult: PaginatedResult<ProductModel []> = new PaginatedResult<ProductModel []>();

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
      if (filter.productManufacturerId > 0) {
        params = params.append('productManufacturerId', filter.productManufacturerId);
      }
    }

    return this.http.get<ProductModel[]>(this.baseUrl + this.controllerPath, { observe: 'response', params})
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

  getProductModel(id) {
    return this.http.get(this.baseUrl + this.controllerPath + '/' + id);
  }

  updateProductModel(element: ProductModel) {
    return this.http.put(this.baseUrl + this.controllerPath + '/' + element.id, element);
  }

  deativateProductModel(id: number) {
    return this.http.post(this.baseUrl + this.controllerPath + '/' + id + '/deactivate', {});
  }

  addProductModel(element: ProductModel) {
    return this.http.post(this.baseUrl + this.controllerPath + '/', element);
  }
}
