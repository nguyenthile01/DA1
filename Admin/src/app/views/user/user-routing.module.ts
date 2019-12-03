import { NgModule, Component } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AdminComponent } from './admin/admin.component';
import { EmployerComponent } from './employer/employer.component';
import { JobSeekerComponent } from './job-seeker/job-seeker.component';
import { DetailEmployerComponent } from './detail-employer/detail-employer.component';
import { DetailJobseekerComponent } from './job-seeker/detail-jobseeker/detail-jobseeker.component';


const routes: Routes = [
  {
    path: '',
    data: {
      title: 'User'
    },
    children: [
      {
        path: '',
        redirectTo: 'user'
      },
      {
        path: 'admin',
        component: AdminComponent,
        data: {
          title: 'Admin'
        }
      },
      {
        path: 'employer',
        component: EmployerComponent,
        data: {
          title: 'Employer'
        },
        children:[

        ]
      },
      {
        path: 'jobSeeker',
        component: JobSeekerComponent,
        data: {
          title: 'JobSeeker'
        },
        
      },
      {
        path: 'user/jobSeeker/:id',
        component: DetailJobseekerComponent,
        data: {
          title: 'Detail'
        },
        
      },
      {
        path: 'user/employer/:id',
        component: DetailEmployerComponent,
        data: {
          title: 'Detail'
        },
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
