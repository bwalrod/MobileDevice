import { DeviceDate } from './../_models/devicedate';
import { PaginatedResult } from './../_models/pagination';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class DevicedateService {

  baseUrl = environment.apiUrl;
  controllerPath = 'devicedate';

  constructor(private http: HttpClient) { }

  getDeviceDates(page?: number, itemsPerPage?: number, filter?: any, status?: number):
    Observable<PaginatedResult<DeviceDate []>> {
    const paginatedResult: PaginatedResult<DeviceDate []> = new PaginatedResult<DeviceDate []>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('page', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    if (filter != null) {
      // params = params.append('dateValue', filter.dateValue);
      params = params.append('deviceId', filter.deviceId);
      if (filter.dateTypeId > 0) {
        params = params.append('dateTypeId', filter.dateTypeId);
      }
      if (filter.dateValue != null) {
        params = params.append('dateValue', filter.dateValue);
      }

    }

    if (status != null) {
      params = params.append('active', status.toString());
    }

    return this.http.get<DeviceDate[]>(this.baseUrl + this.controllerPath, { observe: 'response', params})
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

  getDeviceDate(id) {
    return this.http.get(this.baseUrl + this.controllerPath + '/' + id);
  }

  updateDeviceDate(element: DeviceDate) {
    return this.http.put(this.baseUrl + this.controllerPath + '/' + element.id, element);
  }

  deactivateDeviceDate(id: number) {
    return this.http.post(this.baseUrl + this.controllerPath + '/' + id + '/deactivate', {});
  }

  addDeviceDate(element: DeviceDate) {
    return this.http.post(this.baseUrl + this.controllerPath + '/', element);
  }

}
