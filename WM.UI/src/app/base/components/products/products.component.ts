import { Component, OnDestroy, OnInit } from "@angular/core";
import { MatTableDataSource } from "@angular/material/table"

@Component({
    selector: 'app-products',
    templateUrl: './products.component.html',
    styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit, OnDestroy {
    data = new MatTableDataSource<string>();

    ngOnDestroy(): void {
        
    }
    
    ngOnInit(): void {
        this.data.data =  ['1', '2', '3']
    }
    
}