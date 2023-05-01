import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Product } from "../models/Product";
import { ProductFilter } from "../models/ProductFilter";

@Injectable({
    providedIn: 'root',
})
export class ProductDataService {
    constructor(private http: HttpClient) { }

    getProducts(filter: ProductFilter) {
        return this.http.post<Product[]>("https://localhost:44369/api/products/get", {
            name: filter.name,
            minPrice: filter.minPrice,
            maxPrice: filter.maxPrice
        });
    }

    getProductById(id: number) {
        return this.http.get<Product[]>("https://localhost:44369/api/products/id", {
            params: {
                id: id,
            }
        });
    }

    createProduct(product: Product) {
        return this.http.post<Product>("https://localhost:44369/api/products", {
            name: product.name,
            description: product.description,
            price: product.price
        })
    }

    updateProduct(product: Product) {
        return this.http.put<Product>("https://localhost:44369/api/products", {
            id: product.id,
            name: product.name,
            description: product.description,
            price: product.price
        })
    }

    deleteProduct(id: number) {
        return this.http.delete<number>(`https://localhost:44369/api/products/${id}`);
    }
}