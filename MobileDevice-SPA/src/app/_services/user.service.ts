import { User } from './../_models/user';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';

@Injectable({
    providedIn: 'root'
})
export class UserService {
    baseUrl = environment.apiUrl;

    constructor(private http: HttpClient) { }

    getUsers() {
        return this.http.get(this.baseUrl + 'user');
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
