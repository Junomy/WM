import { Component, OnDestroy, OnInit } from "@angular/core";
import { finalize, ReplaySubject, takeUntil } from "rxjs";
import { MenuItem } from "../../models/MenuItem";
import { NavigationDataService } from "../../services/navigation-data.service";

@Component({
    selector: 'app-navigation',
    templateUrl: './navigation.component.html',
    styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent implements OnInit, OnDestroy {
    menuItems: MenuItem[] = [];
    loading = false;
    
    destroy$ = new ReplaySubject<void>(1);

    constructor (private dataService: NavigationDataService) {
    }

    ngOnInit(): void {
        this.loading = true;
        this.dataService.getMenuItems()
            .pipe(
                takeUntil(this.destroy$), 
                finalize(() => this.loading = false))
            .subscribe(res => this.menuItems = res);
        this.loading = false;
    }

    ngOnDestroy(): void {
        this.destroy$.next();
        this.destroy$.complete();
    }
    
}