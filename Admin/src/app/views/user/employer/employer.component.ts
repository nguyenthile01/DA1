import { Component, OnInit } from '@angular/core';
import { EmployerService } from './employer.service';
import { Employer } from '../user.model';

@Component({
  selector: 'app-employer',
  templateUrl: './employer.component.html',
  styleUrls: ['./employer.component.css']
})
export class EmployerComponent implements OnInit {
  public data: [];
  public total: 0;
  public filter: {
    name: 'abc';
    nameCompany: ''

  }
  
  constructor(private dataService: EmployerService) { }
  
  loadData(){
    this.dataService.getEmployer(this.filter).subscribe(data=>{
      this.data = data.result.items;
      this.total = data.result.totalCount;
      console.log(this.filter)
    }),(error=>{
      
    })
  }
  
  ngOnInit() {
    this.loadData();
  }

}
