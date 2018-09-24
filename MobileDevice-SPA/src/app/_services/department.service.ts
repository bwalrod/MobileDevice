import { UserEditComponent } from './../user-edit/user-edit.component';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { PaginatedResult } from '../_models/pagination';
import { Department } from '../_models/department';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getDepartments(page?, itemsPerPage?, filter?): Observable<PaginatedResult<Department []>> {
    const paginatedResult: PaginatedResult<Department []> = new PaginatedResult<Department []>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('page', page);
      params = params.append('pageSize', itemsPerPage);
    }

    if (filter != null) {
      params = params.append('name', filter);
    }

    return this.http.get<Department[]>(this.baseUrl + 'department', { observe: 'response', params})
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

  getDepartment(id) {
    return this.http.get(this.baseUrl + 'department/' + id);
  }

  updateDepartment(department: Department) {
    return this.http.put(this.baseUrl + 'department/' + department.id, department);
  }

  deactivateDepartment(id: number) {
    return this.http.post(this.baseUrl + 'department/' + id + '/deactivate', {});
  }

  addDepartment(department: Department) {
    return this.http.post(this.baseUrl + 'department/', department);
  }

}
