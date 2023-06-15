import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, map } from "rxjs";
import { TokenResult } from "../models/TokenResult";
import { User } from "../models/User";

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    constructor(private http: HttpClient) {}

    public getJwtToken() {
        return localStorage.getItem('token');
    }

    public login(email: string, password: string) {
        const headers = new HttpHeaders().set('disable-auth-interceptor', '');
        return this.http.post<TokenResult>(`https://localhost:44369/api/users/login`, { email, password }, { headers, responseType: 'json' });
    }

    public logout() {
        localStorage.removeItem('token');
        localStorage.removeItem('expiresAt');
    }

    public hasExpired() {
        var expiresAt = localStorage.getItem('expiresAt');
        if(expiresAt != null)
            return new Date() >= new Date(expiresAt);
        return true;
    }

    public getCurrentUser() {
        return this.http.get<User>(`https://localhost:44369/api/users`);
    }
}