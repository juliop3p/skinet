import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BasketService } from "src/app/basket/basket.service";
import { ShopService } from './../shop.service';
import { IProduct } from './../../shared/models/product';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct;
  quantity = 1;

  constructor(
    private shopService: ShopService,
    private activatedRoute: ActivatedRoute,
    private bcService: BreadcrumbService,
    private basketService: BasketService
  ) {
    this.bcService.set('@productDetails', ' ');
  }

  ngOnInit(): void {
    this.loadProduct();
  }

  addItemToBasket() {
    this.basketService.addItemToBasket(this.product, this.quantity);
  }

  incrementQuantity() {
    this.quantity++;
  }

  decrementQuantity() {
    if(this.quantity > 1) {
      this.quantity--;
    }
  }

  loadProduct() {
    const id = Number(this.activatedRoute.snapshot.paramMap.get('id'));

    this.shopService.getProduct(id).subscribe(
      (product) => {
        this.product = product;
        this.bcService.set('@productDetails', product.name);
      },
      (error) => {
        console.log(error);
      }
    );
  }
}
