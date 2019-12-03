import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from "@angular/forms";


import { DetailJobRoutingModule } from './detail-job-routing.module';
import { DetailJobComponent } from './detail-job.component';

import { EditorModule } from '@tinymce/tinymce-angular';
import { BsDatepickerModule } from 'ngx-bootstrap';


@NgModule({
  declarations: [DetailJobComponent],
  imports: [
    CommonModule,
    DetailJobRoutingModule,
    FormsModule,
    EditorModule,
    BsDatepickerModule,
  ],
  bootstrap: [DetailJobComponent]
})
export class DetailJobModule { }
