import { Component, OnInit, Output, EventEmitter, AfterViewInit } from "@angular/core";
import { AuthService } from "../../services/auth.service";
import { Router } from "@angular/router";
import { Form, FormControl, FormGroup } from "@angular/forms";

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
    loading = false;
    fg!: FormGroup;
    @Output() loggedIn = new EventEmitter<boolean>();

    constructor(private authService: AuthService, private router: Router) {}

    ngOnInit(): void {
        this.loggedIn.emit(false);
        if(this.authService.getJwtToken()) {
            this.loggedIn.emit(true);
        }
        this.fg = new FormGroup({
            email: new FormControl(),
            password: new FormControl()
        })
    }

    login() {
        console.log('start')
        this.loading = true;
        const email = this.fg.value.email;
        const password = this.fg.value.password;

        this.authService.login(email, password).subscribe(res => {
            localStorage.setItem('token', res.token);
            localStorage.setItem('expiresAt', res.expiresAt);
            this.loggedIn.emit(true);
            this.router.navigate(['/dashboard']);
            this.loading = false;
        });
    }
}