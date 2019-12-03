import { Component, OnInit } from '@angular/core';
import { EmployerService } from '../employer/employer.service';
import {Router, ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-detail-employer',
  templateUrl: './detail-employer.component.html',
  styleUrls: ['./detail-employer.component.css']
})
export class DetailEmployerComponent implements OnInit {
  public detail: any = {};
  id: '';

  constructor(
    private dataService: EmployerService,
    private route: ActivatedRoute
  ) {
    this.route.params.subscribe( params => {
      this.id = params.id;
    } );
   }

    //function
  getForEdit(id){
    this.dataService.getForEdit(id).subscribe(data=>{
      this.detail = data.result;
    })
  }
  createAndUpdateItem(){
    this.dataService.createOrUpdate(this.detail).subscribe(data=>{
      alert("Create success!")
    })
  }
  isNumberKey(evt) {
    evt = evt ? evt : window.event;
    var charCode = evt.which ? evt.which : evt.keyCode;
    if (
      charCode > 31 &&
      (charCode < 48 || charCode > 57) &&
      charCode !== 46
    ) {
      evt.preventDefault();
    } else {
      return true;
    }
  }
  ngOnInit() {
    this.getForEdit(this.id)
  }

}
