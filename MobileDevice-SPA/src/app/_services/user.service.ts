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
    currentUser: User;

    constructor(private http: HttpClient) { }

    getUsers(page?, itemsPerPage?, filter?: any, status?: number): Observable<PaginatedResult<User []>> {
        const paginatedResult: PaginatedResult<User []> = new PaginatedResult<User[]>();

        let params = new HttpParams();

        if (page != null && itemsPerPage != null) {
            params = params.append('page', page);
            params = params.append('pageSize', itemsPerPage);
        }

        if (status != null) {
            params = params.append('active', status.toString());
        }

        if (filter != null) {
            params = params.append('login', filter.login);
            params = params.append('firstname', filter.firstname);
            params = params.append('lastname', filter.lastname);
            params = params.append('accesslevel', filter.accesslevel);
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

    identifyUser() {
        return this.http.get(this.baseUrl + 'user/identify')
        .pipe(
            map((response: any) => {
                const user = response;
                if (user) {
                    localStorage.setItem('user', JSON.stringify(user));
                    this.currentUser = user;
                }
            })
        );
    }

    addUser(user: User) {
        return this.http.post(this.baseUrl + 'user/', user);
    }
}
