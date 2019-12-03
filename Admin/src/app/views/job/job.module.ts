import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";

import { ModalModule } from "ngx-bootstrap/modal";
import { PaginationModule } from "ngx-bootstrap/pagination";
import { NgxPaginationModule } from "ngx-pagination";
import { BsDropdownModule } from "ngx-bootstrap/dropdown";
import { ButtonsModule } from "ngx-bootstrap/buttons";
import { EditorModule } from '@tinymce/tinymce-angular';
import { BsDatepickerModule } from 'ngx-bootstrap';

import { JobRoutingModule } from "./job-routing.module";
import { JobComponent } from "./job.component";
import { CategoryComponent } from './category/category.component';
import { DetailJobComponent } from './detail-job/detail-job.component';

@NgModule({
  declarations: [JobComponent, CategoryComponent, DetailJobComponent],
  imports: [
    CommonModule,
    JobRoutingModule,
    FormsModule,
    BsDropdownModule,
    ButtonsModule.forRoot(),
    ModalModule.forRoot(),
    PaginationModule.forRoot(),
    NgxPaginationModule,
    EditorModule,
    BsDatepickerModule,
  ],
  bootstrap:[JobComponent, CategoryComponent, DetailJobComponent]
})
export class JobModule {}
