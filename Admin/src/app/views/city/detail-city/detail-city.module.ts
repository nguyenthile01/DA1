import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from "@angular/forms";

import { DetailCityRoutingModule } from './detail-city-routing.module';
import { DetailCityComponent } from './detail-city.component';


@NgModule({
  declarations: [DetailCityComponent],
  imports: [
    CommonModule,
    DetailCityRoutingModule,
    FormsModule
  ],
  bootstrap: [DetailCityComponent]
})
export class DetailCityModule { }
