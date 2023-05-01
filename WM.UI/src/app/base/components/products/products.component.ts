import { Component, OnDestroy, OnInit, ViewChild } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { finalize, ReplaySubject, takeUntil } from "rxjs";
import { Product } from "../../models/Product";
import { ProductDataService } from "../../services/product-data.service";
import { ProductDialogComponent } from "../product-dialog/product-dialog.component";
import { ProductFilter } from "../../models/ProductFilter";
import { FormControl, FormGroup } from "@angular/forms";

@Component({
    selector: 'app-products',
    templateUrl: './products.component.html',
    styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit, OnDestroy {
    products = new MatTableDataSource<Product>();
    displayedColumns = ['name', 'description', 'price', 'editBtn'];
    pageSize = 10;
    loading = false;
    filter = new ProductFilter();
    filterGroup!: FormGroup;
    @ViewChild(MatSort, { static: true }) sort!: MatSort;
    @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
    
    destroy$ = new ReplaySubject<void>(1);

    constructor(private productsDataService: ProductDataService,
        private dialog: MatDialog) { }
    
    ngOnInit(): void {
        this.loading = true;
        this.filter.name = null;
        this.filter.minPrice = null;
        this.filter.maxPrice = null;
        this.productsDataService.getProducts(this.filter)
            .pipe(
                takeUntil(this.destroy$), 
                finalize(() => this.loading = false))
            .subscribe(res => {
                this.products.data = res
                this.filterGroup = new FormGroup({
                    name: new FormControl(),
                    minPrice: new FormControl(
                        this.products.data.reduce((prev, current) => {
                            return (prev.price < current.price) ? prev : current;
                        }, { price: 0 }).price
                    ),
                    maxPrice: new FormControl(this.products.data.reduce((prev, current) => {
                            return (prev.price > current.price) ? prev : current;
                        }, { price: 0 }).price
                    )
                });
            })
        
        this.products.sort = this.sort;
        this.products.paginator = this.paginator;
              
        this.filterGroup = new FormGroup({
            name: new FormControl(),
            minPrice: new FormControl(
                this.products.data.reduce((prev, current) => {
                    return (prev.price < current.price) ? prev : current;
                }, { price: 0 }).price
            ),
            maxPrice: new FormControl(this.products.data.reduce((prev, current) => {
                    return (prev.price > current.price) ? prev : current;
                }, { price: 0 }).price
            )
        });

        this.loading = false;
    }

    ngOnDestroy(): void {
        this.destroy$.next();
        this.destroy$.complete();
    }

    addProduct(product: Product | null) {
        const dialogRef = this.dialog.open(ProductDialogComponent, {
            data: product,
            width: '45%',
            minWidth: '550px',
            height: '300px'
        })

        dialogRef.afterClosed().subscribe((res) => {
            console.log(res.name)
            let updatedProducts = this.products.data;
            if(updatedProducts.some(x => x.id == res.id)){
                updatedProducts = updatedProducts.map(x => {
                    if(x.id == res.id) {
                        console.log("Found!")
                        return {
                            id: res.id,
                            name: res.name,
                            description: res.description,
                            price: res.price
                        };
                    }
                    return x;
                })
            }
            else {
                updatedProducts.push(res)
            }
            this.products.data = updatedProducts;
        })
    }

    deleteProduct(id: number) {
        this.productsDataService.deleteProduct(id)
            .subscribe((res) => {
                if(this.products.data.some(x => x.id == res)) {
                    this.products.data = this.products.data.filter(x => x.id != res)
                }
            });
    }

    search() {
        this.loading = true;
        this.filter.name = this.filterGroup.value.name;
        this.filter.minPrice = this.filterGroup.value.minPrice;
        this.filter.maxPrice = this.filterGroup.value.maxPrice;
        this.productsDataService.getProducts(this.filter)
            .pipe(
                takeUntil(this.destroy$), 
                finalize(() => this.loading = false))
            .subscribe(res => this.products.data = res)
    }

    clearFilters() {
        this.filterGroup.controls['name'].setValue(null);
        this.filterGroup.controls['minPrice'].setValue(null);
        this.filterGroup.controls['maxPrice'].setValue(null);

        this.search()
    }
}