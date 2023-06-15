import { Component, Inject, OnDestroy, OnInit } from "@angular/core";
import { FormControl, FormGroup } from "@angular/forms";
import { ReplaySubject } from "rxjs";
import { WarehouseDataService } from "../../services/warehouse-data.service";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { Warehouse } from "../../models/Warehouse";
import { Facility } from "../../models/Facility";
import { FacilityDataService } from "../../services/facility-data.service";

@Component({
    selector: 'app-warehouse-dialog',
    templateUrl: './warehouse-dialog.component.html',
    styleUrls: ['./warehouse-dialog.component.scss']
})
export class WarehouseDialogComponent implements OnInit, OnDestroy {
    loading = false;
    destroy$ = new ReplaySubject<void>(1);
    facilities: Facility[] = [];
    fg!: FormGroup;

    constructor(private warehouseDataService: WarehouseDataService,
        private facilityDataService: FacilityDataService,
        public dialogRef: MatDialogRef<WarehouseDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public warehouse: Warehouse) {}

    ngOnInit(): void {
        this.loading = true;
        this.facilityDataService.getFacilities()
            .subscribe(res => {
                this.facilities = res;
            })
        this.fg = new FormGroup({
            name: new FormControl(),
            description: new FormControl(),
            facility: new FormControl()
        })
        if(this.warehouse != null) {
            this.fg.setValue({
                name: this.warehouse.name,
                description: this.warehouse.description,
                facility: this.warehouse.facilityId
            })
        }
        
        this.loading = false;
    }

    ngOnDestroy(): void {
        this.destroy$.next();
        this.destroy$.complete();
    }

    save() {
        const result = new Warehouse();
        if(this.warehouse) {
            const updatedWarehouse = new Warehouse();
            updatedWarehouse.id = this.warehouse.id;
            updatedWarehouse.name = this.fg.value.name;
            updatedWarehouse.description = this.fg.value.description;
            console.log(updatedWarehouse)
            this.warehouseDataService.updateWarehouse(updatedWarehouse).subscribe();
        }
        else {
            const newWarehouse = new Warehouse();
            newWarehouse.name = this.fg.value.name;
            newWarehouse.description = this.fg.value.description;
            newWarehouse.facilityId = this.fg.value.facility;
            this.warehouseDataService.createWarehouse(newWarehouse).subscribe();
        }

        this.dialogRef.close();
    }
}