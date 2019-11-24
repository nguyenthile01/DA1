import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminComponent } from './admin/admin.component';
import { EmployerComponent } from './employer/employer.component';
import { JobSeekerComponent } from './job-seeker/job-seeker.component';


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
        }
      },
      {
        path: 'jobSeeker',
        component: JobSeekerComponent,
        data: {
          title: 'JobSeeker'
        }
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
