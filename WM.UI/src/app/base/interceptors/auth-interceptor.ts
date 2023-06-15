import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { EventEmitter, Injectable, Output } from "@angular/core";
import { Observable } from "rxjs";
import { AuthService } from "../services/auth.service";
import { Router } from "@angular/router";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    @Output() loggedIn = new EventEmitter<boolean>();

    constructor(private authService: AuthService,
        private router: Router) {}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const token = this.authService.getJwtToken();
        if(this.authService.hasExpired()){
            this.authService.logout();
            this.loggedIn.emit(false);
            this.router.navigate(['']);
        }
        else if(token) {
            req = req.clone({
                setHeaders: {
                    Authorization: `Bearer ${token}`
                }
            });
        }
        else {
            this.loggedIn.emit(false);
            this.router.navigate(['']);
        }

        return next.handle(req);
    }
}