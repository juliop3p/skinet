import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OrderDateiledComponent } from './order-dateiled/order-dateiled.component';
import { OrdersComponent } from './orders.component';

const routes: Routes = [
  {
    path: '',
    component: OrdersComponent,
    data: { breadcrumb: { alias: 'Orders' } },
  },
  {
    path: ':id',
    component: OrderDateiledComponent,
    data: { breadcrumb: { alias: 'OrderDetailed' } },
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class OrdersRoutingModule {}
