import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { UserManagerComponent } from './userManager.component';

const routes: Routes = [
    {
      path: '',
      data: {
        title: 'Admin'
      },
      children: [
        {
          path: '',
          redirectTo: 'admin'
        },
        {
          path: 'admin',
          component: UserManagerComponent,
          data: {
            title: 'Admin'
          }
        },
        // {
        //   path: 'dropdowns',
        //   component: DropdownsComponent,
        //   data: {
        //     title: 'Dropdowns'
        //   }
        // },
        // {
        //   path: 'brand-buttons',
        //   component: BrandButtonsComponent,
        //   data: {
        //     title: 'Brand buttons'
        //   }
        // }
      ]
    }
  ];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserManagerRoutingModule {}
