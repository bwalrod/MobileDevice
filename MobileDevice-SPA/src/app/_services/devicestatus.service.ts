import { DeviceStatus } from './../_models/DeviceStatus';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class DeviceStatusService {
  baseUrl = environment.apiUrl;
  controllerPath = 'devicestatus';

  constructor(private http: HttpClient) {}

  getDeviceStatuses(
    page?: number,
    itemsPerPage?: number,
    filter?: string,
    status?: number
  ): Observable<PaginatedResult<DeviceStatus[]>> {
    const paginatedResult: PaginatedResult<
      DeviceStatus[]
    > = new PaginatedResult<DeviceStatus[]>();

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

    return this.http
      .get<DeviceStatus[]>(this.baseUrl + this.controllerPath, {
        observe: 'response',
        params
      })
      .pipe(
        map(response => {
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(
              response.headers.get('Pagination')
            );
          }
          return paginatedResult;
        })
      );
  }

  getDeviceStatus(id) {
    return this.http.get(this.baseUrl + this.controllerPath + '/' + id);
  }

  updateDeviceStatus(deviceStatus: DeviceStatus) {
    return this.http.put(
      this.baseUrl + this.controllerPath + '/' + deviceStatus.id,
      deviceStatus
    );
  }

  deactivateDeviceStatus(id: number) {
    return this.http.post(
      this.baseUrl + this.controllerPath + '/' + id + '/deactivate',
      {}
    );
  }

  addDeviceStatus(deviceStatus: DeviceStatus) {
    return this.http.post(
      this.baseUrl + this.controllerPath + '/',
      deviceStatus
    );
  }
}
