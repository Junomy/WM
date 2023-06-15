import { Component, OnDestroy, OnInit, ViewChild } from "@angular/core";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { ReplaySubject, finalize, takeUntil } from "rxjs";
import { Facility } from "../../models/Facility";
import { FacilityDataService } from "../../services/facility-data.service";
import { Warehouse } from "../../models/Warehouse";

@Component({
    selector: 'app-facilities',
    templateUrl: './facilities.component.html',
    styleUrls: ['./facilities.component.scss'],
})
export class FacilitiesComponent implements OnInit, OnDestroy {
    loading = false;
    destroy$ = new ReplaySubject<void>(1)
    pageSize = 10;
    @ViewChild(MatSort, { static: true }) sort!: MatSort;
    @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
    facilities = new MatTableDataSource<Facility>();
    warehouses = new MatTableDataSource<Warehouse>();
    displayedColumns = ['name', 'description', 'address'];
    warehouseColumns = ['name', 'description'];

    constructor(private facilityService: FacilityDataService) {}

    ngOnInit(): void {
        this.loading = true;
        this.facilityService.getFacilities()
            .pipe(
                takeUntil(this.destroy$),
                finalize(() => this.loading = false)
            )
            .subscribe((res) => {
                this.facilities.data = res;
            })
    }

    ngOnDestroy(): void {
        this.destroy$.next();
        this.destroy$.complete();
    }

    toggleWarehouses(facility: Facility) {
        if(this.isExpanded(facility)) {
            this.warehouses.data = [];
            console.log(this.warehouses.data)
        } else {
            this.warehouses.data = facility.warehouses;
            console.log(this.warehouses.data)
        }
    }

    isExpanded = (item: Facility) => {
        return this.warehouses.data.length && this.warehouses.data[0].facilityId === item.id;
    };

    getWarehouses(facility) {
        const dataSource = new MatTableDataSource<Warehouse>(facility.warehouses);
        return dataSource;
    }
}