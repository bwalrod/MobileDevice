import { Observable } from 'rxjs';
import { PaginatedResult } from './../_models/pagination';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { Product } from '../_models/product';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  baseUrl = environment.apiUrl;
  controllerPath = 'product';

  constructor(private http: HttpClient) { }

  getAllProducts(filter?: any, status?: number): Observable<Product[]> {
    let results: Product[];
    let params = new HttpParams();

    if (status != null) {
      params = params.append('active', status.toString());
    }

    if (filter != null) {
      params = params.append('productTypeId', filter.productTypeId);
    }

    return this.http.get<Product[]>(this.baseUrl + this.controllerPath + '/all', { observe: 'response', params})
    .pipe(
      map(response => {
        results = response.body;
        return results;
      })
    );
  }

  getProducts(page?: number, itemsPerPage?: number, filter?: any, status?: number): Observable<PaginatedResult<Product []>> {
    const paginatedResult: PaginatedResult<Product []> = new PaginatedResult<Product []>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('page', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    if (status != null) {
      params = params.append('active', status.toString());
    }

    if (filter != null) {
      params = params.append('partNum', filter.partNum);
      if (filter.productModelId > 0) {
        params = params.append('productModelId', filter.productModelId);
      }
      if (filter.productCapacityId > 0) {
        params = params.append('productCapacityId', filter.productCapacityId);
      }
      if (filter.productCapacityName != null) {
        params = params.append('productCapacityName', filter.productCapacityName);
      }
      if (filter.productTypeId > 0) {
        params = params.append('productTypeId', filter.productTypeId);
      }
      if (filter.productManufacturerId > 0) {
        params = params.append('productManufacturerId', filter.productManufacturerId);
      }
    }

    return this.http.get<Product[]>(this.baseUrl + this.controllerPath, { observe: 'response', params})
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

  getProduct(id) {
    return this.http.get(this.baseUrl + this.controllerPath + '/' + id);
  }

  updateProduct(element: Product) {
    return this.http.put(this.baseUrl + this.controllerPath + '/' + element.id, element);
  }

  deativateProduct(id: number) {
    return this.http.post(this.baseUrl + this.controllerPath + '/' + id + '/deactivate', {});
  }

  addProduct(element: Product) {
    return this.http.post(this.baseUrl + this.controllerPath + '/', element);
  }
}
