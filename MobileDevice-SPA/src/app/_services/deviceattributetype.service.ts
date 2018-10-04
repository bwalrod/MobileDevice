import { Observable } from 'rxjs';
import { DeviceAttributeType } from './../_models/deviceattributetype';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class DeviceAttributeTypeService {

  baseUrl = environment.apiUrl;
  controllerPath = 'deviceattributetype';

  constructor(private http: HttpClient) { }

  getDeviceAttributeTypes(page?: number, itemsPerPage?: number, filter?: string, status?: number):
    Observable<PaginatedResult<DeviceAttributeType []>> {
    const paginatedResult: PaginatedResult<DeviceAttributeType []> = new PaginatedResult<DeviceAttributeType []>();

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

    return this.http.get<DeviceAttributeType[]>(this.baseUrl + this.controllerPath, { observe: 'response', params})
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

  getDeviceAttributeType(id) {
    return this.http.get(this.baseUrl + this.controllerPath + '/' + id);
  }

  updateDeviceAttributeType(deviceAttributeType: DeviceAttributeType) {
    return this.http.put(this.baseUrl + this.controllerPath + '/' + deviceAttributeType.id, deviceAttributeType);
  }

  deactivateDeviceAttributeType(id: number) {
    return this.http.post(this.baseUrl + this.controllerPath + '/' + id + '/deactivate', {});
  }

  addDeviceAttributeType(deviceAttributeType: DeviceAttributeType) {
    return this.http.post(this.baseUrl + this.controllerPath + '/', deviceAttributeType);
  }
}
