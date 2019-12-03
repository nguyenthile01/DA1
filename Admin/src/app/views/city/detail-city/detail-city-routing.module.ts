import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DetailCityComponent } from './detail-city.component';


const routes: Routes = [
  {
    path: '',
    component: DetailCityComponent,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DetailCityRoutingModule { }
