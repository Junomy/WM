import { Component, Inject, OnDestroy, OnInit } from "@angular/core";
import { ReplaySubject } from "rxjs";
import { Product } from "../../models/Product";
import { ProductDataService } from "../../services/product-data.service";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { FormControl, FormGroup } from "@angular/forms";

@Component({
    selector: 'app-product-dialog',
    templateUrl: './product-dialog.component.html',
    styleUrls: ['./product-dialog.component.scss']
})
export class ProductDialogComponent implements OnInit, OnDestroy {
    loading = false;
    destroy$ = new ReplaySubject<void>(1);
    fg!: FormGroup;

    constructor(private dataService: ProductDataService,
        public dialogRef: MatDialogRef<ProductDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public product: Product) { }

    ngOnInit(): void {
        this.loading = true;
        this.fg = new FormGroup({
            name: new FormControl(),
            description: new FormControl(),
            price: new FormControl()
        });
        if(this.product) {
            this.fg.setValue({
                name: this.product.name,
                description: this.product.description,
                price: this.product.price
            })
        }
        this.loading = false;
    }

    ngOnDestroy(): void {
        this.destroy$.next();
        this.destroy$.complete();
    }

    save() {
        const result = new Product();
        if(this.product) {
            const updatedProduct = new Product();
            updatedProduct.id = this.product.id;
            updatedProduct.name = this.fg.value.name;
            updatedProduct.description = this.fg.value.description;            
            updatedProduct.price = this.fg.value.price;
            this.dataService.updateProduct(updatedProduct)
                .subscribe((res) => {
                    result.id = res.id,
                    result.name = res.name,
                    result.description = res.description,
                    result.price = res.price
                })
        }
        else {
            const newProduct = new Product();
            newProduct.name = this.fg.value.name;
            newProduct.description = this.fg.value.description;            
            newProduct.price = this.fg.value.price;
            this.dataService.createProduct(newProduct)
                .subscribe((res) => {
                    result.id = res.id,
                    result.name = res.name,
                    result.description = res.description,
                    result.price = res.price
                })
        }

        this.dialogRef.close(result);
    }

}