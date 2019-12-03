import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';


import { UserRoutingModule } from './user-routing.module';
import { AdminComponent } from './admin/admin.component';
import { EmployerComponent } from './employer/employer.component';
import { JobSeekerComponent } from './job-seeker/job-seeker.component';
import { DetailEmployerComponent } from './detail-employer/detail-employer.component';
import { DataService } from '../../data.service';
import { EmployerService } from './employer/employer.service';

import {HttpClientModule} from '@angular/common/http'
import {MatTableModule} from '@angular/material/table';
import {MatInputModule} from '@angular/material/input';

import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { ModalModule } from 'ngx-bootstrap/modal';
import { FormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import {NgxPaginationModule} from 'ngx-pagination';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

// RECOMMENDED
// import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
// or
import { BsDatepickerModule } from 'ngx-bootstrap';
// import {NgbPaginationConfig} from ''
import { from } from 'rxjs';
import { DetailJobseekerComponent } from './job-seeker/detail-jobseeker/detail-jobseeker.component';
@NgModule({
  declarations: [
    AdminComponent, 
    EmployerComponent, 
    JobSeekerComponent,
    DetailEmployerComponent,
    DetailJobseekerComponent,
  ],
  imports: [
    CommonModule,
    UserRoutingModule,
    HttpClientModule,
    MatTableModule,
    MatInputModule,
    BsDropdownModule,
    FormsModule,
    ButtonsModule.forRoot(),
    ModalModule.forRoot(),
    PaginationModule.forRoot(),
    NgxPaginationModule,
    // BrowserAnimationsModule,
    BsDatepickerModule.forRoot(),
  ],
  providers:[
    DataService,
    EmployerService
  ],
})
export class UserModule { }
