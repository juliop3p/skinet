import { ActivatedRoute } from '@angular/router';
import { OrdersService } from './../orders.service';
import { Component, OnInit } from '@angular/core';
import { IOrder } from 'src/app/shared/models/order';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-order-dateiled',
  templateUrl: './order-dateiled.component.html',
  styleUrls: ['./order-dateiled.component.scss'],
})
export class OrderDateiledComponent implements OnInit {
  order: IOrder;

  constructor(
    private route: ActivatedRoute,
    private ordersService: OrdersService,
    private breadcrumbService: BreadcrumbService
  ) {
    this.breadcrumbService.set('@OrderDetailed', ' ');
  }

  ngOnInit(): void {
    this.ordersService
      .getOrderDetailed(+this.route.snapshot.paramMap.get('id'))
      .subscribe(
        (order: IOrder) => {
          this.order = order;
          this.breadcrumbService.set(
            '@OrderDetailed',
            `Order# ${order.id} - ${order.status}`
          );
        },
        (error) => {
          console.log(error);
        }
      );
  }

  getOrderById(id: number) {
    this.ordersService.getOrderDetailed(id).subscribe();
  }
}
