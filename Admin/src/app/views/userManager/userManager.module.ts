import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import {HttpClientModule} from '@angular/common/http';
import {CommonModule} from '@angular/common';
import { UserManagerComponent } from './userManager.component';

import { UserManagerRoutingModule } from './userManager-routing.module';

@NgModule({
  imports: [
    HttpClientModule,
    FormsModule,
    UserManagerRoutingModule,
    CommonModule,
  ],
  declarations: [ 
    UserManagerComponent,
  ]
})
export class UserManagerModule { }
