import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, map } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    constructor(private http: HttpClient) {}

    public getJwtToken() {
        return localStorage.getItem('token');
    }

    login(email: string, password: string): Observable<string> {
        const headers = new HttpHeaders().set('disable-auth-interceptor', '');
        return this.http.post(`https://localhost:44369/api/users/login`, { email, password }, { headers, responseType: 'text' });
    }

    logout() {
        localStorage.removeItem('token');
    }
}