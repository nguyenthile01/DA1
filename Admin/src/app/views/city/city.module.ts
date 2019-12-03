import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { FormsModule } from "@angular/forms";

import { CityRoutingModule } from "./city-routing.module";
import { CityComponent } from "./city.component";
// import { DetailCityComponent } from './detail-city/detail-city.component';

import { ModalModule } from "ngx-bootstrap/modal";
import { PaginationModule } from "ngx-bootstrap/pagination";
import { NgxPaginationModule } from "ngx-pagination";
import { BsDropdownModule } from "ngx-bootstrap/dropdown";
import { ButtonsModule } from "ngx-bootstrap/buttons";

@NgModule({
  declarations: [CityComponent],
  imports: [
    CommonModule,
    CityRoutingModule,
    FormsModule,
    ModalModule.forRoot(),
    PaginationModule.forRoot(),
    NgxPaginationModule,
    BsDropdownModule,
    ButtonsModule.forRoot()
  ],
  bootstrap: [CityComponent]
})
export class CityModule {}
