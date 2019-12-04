import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { JobComponent } from "./job.component";
import { CategoryComponent } from './category/category.component';
import { JobDetailComponent } from './job-detail/job-detail.component';
// import { DetailJobComponent } from '../detail-job/detail-job.component';
import { DetailJobComponent } from './detail-job/detail-job.component';
import { CategoryDetailComponent } from './category-detail/category-detail.component';


const routes: Routes = [
  {
    path: "",
    data: {
      title: "Job Manager"
    },
    children: [
      {
        path: '',
        redirectTo: ' '
      },
      {
        path: "category",
        component: CategoryComponent,
        data: {
          title: "Category Job"
        }
      },
      {
        path: "post",
        component: JobComponent,
        data: {
          title: "Post"
        },
        
      },
      {
        path: "detail-category/:id",
        component: CategoryDetailComponent,
        data: {
          title: ""
        },
      },
      {
        path: "detail-job/:id",
        component: DetailJobComponent,
        data: {
          title: ""
        },
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class JobRoutingModule {}
