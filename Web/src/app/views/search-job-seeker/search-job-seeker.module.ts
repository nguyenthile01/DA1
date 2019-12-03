import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormsModule } from '@angular/forms';

import { SearchJobSeekerRoutingModule } from './search-job-seeker-routing.module';
import { DetailComponent } from './detail/detail.component';
import { SearchJobSeekerComponent } from './search-job-seeker.component';
import { from } from 'rxjs';


@NgModule({
  declarations: [DetailComponent, SearchJobSeekerComponent],
  imports: [
    CommonModule,
    SearchJobSeekerRoutingModule,
    FormsModule
  ]
})
export class SearchJobSeekerModule { }
