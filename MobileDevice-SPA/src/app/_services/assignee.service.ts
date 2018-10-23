import { Assignee } from './../_models/assignee';
import { PaginatedResult } from './../_models/pagination';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AssigneeService {


  baseUrl = environment.apiUrl;
  controllerPath = 'assignee';

  constructor(private http: HttpClient) { }

  getAllAssignees(filter?: any, status?: number): Observable<Assignee[]> {
    let results: Assignee[];
    let params = new HttpParams();

    if (status != null) {
      params = params.append('active', status.toString());
    }

    if (filter != null) {
      params = params.append('firstName', filter.firstName);
      params = params.append('lastName', filter.lastName);

      if (filter.departmentId > 0) {
        params = params.append('departmentId', filter.departmentId);
      }
    }

    return this.http.get<Assignee[]>(this.baseUrl + this.controllerPath + '/all', { observe: 'response', params})
    .pipe(
      map(response => {
        results = response.body;
        return results;
      })
    );
  }

  getAssignees(page?: number, itemsPerPage?: number, filter?: any, status?: number): Observable<PaginatedResult<Assignee []>> {
    const paginatedResult: PaginatedResult<Assignee []> = new PaginatedResult<Assignee []>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('page', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    if (status != null) {
      params = params.append('active', status.toString());
    }

    if (filter != null) {
      params = params.append('firstName', filter.firstName);
      params = params.append('lastName', filter.lastName);

      if (filter.departmentId > 0) {
        params = params.append('departmentId', filter.departmentId);
      }
    }

    console.log('assignee-service: ' + params);

    return this.http.get<Assignee[]>(this.baseUrl + this.controllerPath, { observe: 'response', params})
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

  getAssignee(id) {
    return this.http.get(this.baseUrl + this.controllerPath + '/' + id);
  }

  updateAssignee(element: Assignee) {
    return this.http.put(this.baseUrl + this.controllerPath + '/' + element.id, element);
  }

  deactivateAssignee(id: number) {
    return this.http.post(this.baseUrl + this.controllerPath + '/' + id + '/deactivate', {});
  }

  addAssignee(element: Assignee) {
    return this.http.post(this.baseUrl + this.controllerPath + '/', element);
  }
}

