import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormsModule } from '@angular/forms';
import { CreateEmployersComponent } from './create-employers.component';



@NgModule({
  declarations: [
    CreateEmployersComponent
  ],
  imports: [
    CommonModule,
    FormsModule
  ]
})
export class CreateEmployersModule { }
