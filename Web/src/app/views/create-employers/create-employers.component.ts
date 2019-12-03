import { Component, OnInit } from '@angular/core';
import { CreateEmployersService } from './create-employers.service';

@Component({
  selector: 'app-create-employers',
  templateUrl: './create-employers.component.html',
  styleUrls: ['./create-employers.component.scss']
})
export class CreateEmployersComponent implements OnInit {

  public filter: any = {
    nameCompany: '',
    userName: '',
    emailAddress: '',
    phoneNumber: '',
    password: '',
    address: '',
  }

  constructor(
    private dataService: CreateEmployersService
  ) { }

  handleCreate(){
    this.dataService.createEmployer(this.filter).subscribe(data=>{
      console.log('hello')
    })
  }
  ngOnInit() {
  }

}
