import { UserEditComponent } from './../user-edit/user-edit.component';
import { Observable } from 'rxjs';
import { environment } from './../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PaginatedResult } from '../_models/pagination';
import { DeviceDateType } from '../_models/devicedatetype';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class DeviceDateTypeService {

  baseUrl = environment.apiUrl;
  controllerPath = 'devicedatetype';

  constructor(private http: HttpClient) { }

  getDeviceDateTypes(page?: number, itemsPerPage?: number, filter?: string, status?: number):
    Observable<PaginatedResult<DeviceDateType []>> {
    const paginatedResult: PaginatedResult<DeviceDateType []> = new PaginatedResult<DeviceDateType []>();

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

    return this.http.get<DeviceDateType[]>(this.baseUrl + this.controllerPath, { observe: 'response', params})
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

  getDeviceDateType(id) {
    return this.http.get(this.baseUrl + this.controllerPath + '/' + id);
  }

  getAvailableDeviceDateType(deviceId) {
    return this.http.get<DeviceDateType[]>(this.baseUrl + this.controllerPath + '/' + deviceId + '/available');
  }

  updateDeviceDateType(element: DeviceDateType) {
    return this.http.put(this.baseUrl + this.controllerPath + '/' + element.id, element);
  }

  deactivateDeviceDateType(id: number) {
    return this.http.post(this.baseUrl + this.controllerPath + '/' + id + '/deactivate', {});
  }

  addDeviceDateType(element: DeviceDateType) {
    return this.http.post(this.baseUrl + this.controllerPath + '/', element);
  }
}
