import { OrdersService } from './orders.service';
import { Component, OnInit } from '@angular/core';
import { IOrder, IOrderToCreate } from '../shared/models/order';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss'],
})
export class OrdersComponent implements OnInit {
  orders: IOrder[];

  constructor(private ordersService: OrdersService) {}

  ngOnInit(): void {
    this.getOrders();
  }

  getOrders() {
    this.ordersService.getClientOrders().subscribe(
      (orders: IOrder[]) => {
        this.orders = orders;
      },
      (error) => {
        console.log(error);
      }
    );
  }
}
