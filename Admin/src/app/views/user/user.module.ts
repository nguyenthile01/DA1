import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { AdminComponent } from './admin/admin.component';
import { EmployerComponent } from './employer/employer.component';
import { JobSeekerComponent } from './job-seeker/job-seeker.component';
import {HttpClientModule} from '@angular/common/http'
import { DataService } from '../../data.service';
import { EmployerService } from './employer/employer.service';
import {MatTableModule} from '@angular/material/table';
import {MatInputModule} from '@angular/material/input';

@NgModule({
  declarations: [AdminComponent, EmployerComponent, JobSeekerComponent],
  imports: [
    CommonModule,
    UserRoutingModule,
    HttpClientModule,
    MatTableModule,
    MatInputModule
  ],
  providers:[
    DataService,
    EmployerService
  ],
})
export class UserModule { }
