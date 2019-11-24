// Angular
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import {TestComponent} from './test.component';
import {TestRoutingModule} from './test-routing.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    TestRoutingModule,
  ],
  declarations:[TestComponent],
  providers:[],

})
export class testModule { }
