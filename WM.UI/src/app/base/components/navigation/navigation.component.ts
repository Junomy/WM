import { Component, EventEmitter, Inject, OnDestroy, OnInit, Output } from "@angular/core";
import { finalize, ReplaySubject, takeUntil } from "rxjs";
import { MenuItem } from "../../models/MenuItem";
import { NavigationDataService } from "../../services/navigation-data.service";
import { AuthService } from "../../services/auth.service";
import { Router } from "@angular/router";

@Component({
    selector: 'app-navigation',
    templateUrl: './navigation.component.html',
    styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent implements OnInit, OnDestroy {
    menuItems: MenuItem[] = [];
    loading = false;
    @Output() loggedIn = new EventEmitter<boolean>();
    
    destroy$ = new ReplaySubject<void>(1);

    constructor (private dataService: NavigationDataService,
        private authService: AuthService,
        private router: Router) {
    }

    ngOnInit(): void {
        this.loading = true;
        this.loggedIn.emit(true);
        this.dataService.getMenuItems()
            .pipe(
                takeUntil(this.destroy$), 
                finalize(() => this.loading = false))
            .subscribe(res => {
                this.menuItems = res;
                this.loading = false;
            });
    }

    ngOnDestroy(): void {
        this.destroy$.next();
        this.destroy$.complete();
    }
    
    logout() {
        this.loggedIn.emit(false);
        this.authService.logout();
        window.location.reload();
    }
}