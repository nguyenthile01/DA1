import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SearchJobSeekerComponent } from './search-job-seeker.component';
import { DetailComponent } from './detail/detail.component';


const routes: Routes = [
  {
    path: '',
    data: {
      title: 'List Jobseekers'
    },
    children: [
      {
        path: '',
        redirectTo: ' '
      },
      {
        path: 'job-seeker',
        component: SearchJobSeekerComponent,
        data: {
          title: 'List Jobseekers'
        }
      },
      {
        path: 'job-seeker/:id',
        component: DetailComponent,
        data: {
          title: ''
        }
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SearchJobSeekerRoutingModule { }
