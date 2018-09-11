import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class UserService {
    baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }

getUsers() {
    
}

}
