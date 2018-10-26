import { Device } from './../_models/device';
import { PaginatedResult } from 'src/app/_models/pagination';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class DeviceService {
  baseUrl = environment.apiUrl;
  controllerPath = 'device';

  constructor(private http: HttpClient) { }

  getDevices(page?: number, itemsPerPage?: number, filter?: any, status?: number): Observable<PaginatedResult<Device []>> {
    const paginatedResult: PaginatedResult<Device []> = new PaginatedResult<Device []>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('page', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    if (status != null) {
      params = params.append('active', status.toString());
    }

    if (filter != null) {
      params = params.append('serialNumber', filter.serialNumber);
      params = params.append('esn', filter.esn);
      params = params.append('os', filter.os);
      if (filter.productTypeId > 0) {
        params = params.append('productTypeId', filter.productTypeId);
      }
      if (filter.productManufacturerId > 0) {
        params = params.append('productManufacturerId', filter.productManufacturerId);
      }
      if (filter.productModelId > 0) {
        params = params.append('productModelid', filter.productModelId);
      }
      if (filter.assigneeDepartmentId > 0) {
        params = params.append('assigneeDepartmentId', filter.assigneeDepartmentId);
      }
      if (filter.productCapacityId > 0) {
        params = params.append('productCapacityId', filter.productCapacityId);
      }
      if (filter.assigneeId > 0) {
        params = params.append('assigneeId', filter.assigneeId);
      }
      if (filter.statusId > 0) {
        params = params.append('deviceStatusid', filter.statusId);
      }
    }

    return this.http.get<Device[]>(this.baseUrl + this.controllerPath, { observe: 'response', params})
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

  getDevice(id) {
    return this.http.get(this.baseUrl + this.controllerPath + '/' + id);
  }

  updateDevice(element: Device) {
    return this.http.put(this.baseUrl + this.controllerPath + '/' + element.id, element);
  }

  deactivateDevice(id: number) {
    return this.http.post(this.baseUrl + this.controllerPath + '/' + id + '/deactivate', {});
  }

  addDevice(element: Device) {
    return this.http.post(this.baseUrl + this.controllerPath + '/', element);
  }

}
