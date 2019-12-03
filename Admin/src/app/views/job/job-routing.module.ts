import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { JobComponent } from "./job.component";
import { CategoryComponent } from './category/category.component';
import { DetailJobComponent } from './detail-job/detail-job.component';

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
      // {
      //   path: "/:id",
      //   component: DetailJobComponent,
      //   data: {
      //     title: "Post"
      //   },
        
      // },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class JobRoutingModule {}
