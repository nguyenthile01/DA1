import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CreateJobSeekersComponent } from './create-job-seekers.component';


const routes: Routes = [
  {
    path: '',
    component: CreateJobSeekersComponent,
    data: {
      title: ''
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CreateJobSeekersRoutingModule { }
