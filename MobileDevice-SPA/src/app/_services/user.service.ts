import { User } from './../_models/user';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class UserService {
    baseUrl = environment.apiUrl;

    constructor(private http: HttpClient) { }

    getUsers(page?, itemsPerPage?): Observable<PaginatedResult<User []>> {
        const paginatedResult: PaginatedResult<User []> = new PaginatedResult<User[]>();

        let params = new HttpParams();

        if (page != null && itemsPerPage != null) {
            params = params.append('page', page);
            params = params.append('pageSize', itemsPerPage);
        }

        // return this.http.get(this.baseUrl + 'user');
        return this.http.get<User[]>(this.baseUrl + 'user', { observe: 'response', params})
        .pipe(
            map(response => {
                // alert(response.body);
                paginatedResult.result = response.body;
                if (response.headers.get('Pagination') != null) {
                    paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
                    // console.log(paginatedResult.pagination.itemsPerPage);
                    // console.log(paginatedResult.result);
                }
                return paginatedResult;
            })
        );
    }

    getUser(id) {
        return this.http.get(this.baseUrl + 'user/' + id);
    }

    updateUser(user: User) {
        return this.http.put(this.baseUrl + 'user/' + user.id, user, {withCredentials: true});
    }

    deactivateUser(id: number) {
        return this.http.post(this.baseUrl + 'user/' + id + '/deactivate', {});
    }
}
