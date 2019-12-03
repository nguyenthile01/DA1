import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DetailJobComponent } from './detail-job.component';


const routes: Routes = [
  {
    path: '',
    component: DetailJobComponent,
    data: {
      title: 'Detail'
    },
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DetailJobRoutingModule { }
