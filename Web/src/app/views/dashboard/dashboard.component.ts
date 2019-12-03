import { Component, OnInit } from '@angular/core';
import { getStyle, hexToRgba } from '@coreui/coreui/dist/js/coreui-utilities';
import { CustomTooltips } from '@coreui/coreui-plugin-chartjs-custom-tooltips';
import { DashboarService } from './dashboard.service';
import {ILocation, ICategoryJob} from './dashboard.model'
import { Router, ActivatedRoute } from "@angular/router";

@Component({
  templateUrl: 'dashboard.component.html'
})
export class DashboardComponent implements OnInit {

  public filter: any = {
    name: '',
    jobLocationId: '',
    categoryId: '',
    YearsOfExperience: '',
  };
  public dataCategory: [];
  public dataLocation: [];
  public data: [];
  public total: 0;

  constructor(private dataService: DashboarService,
      private router: Router,
    ){}

  loadCategoryJob(){
    this.dataService.getCategoryJob().subscribe(data=>{
      this.dataCategory = data.result.items;
    }), error => {};
  }

  loadLocation(){
    this.dataService.getLocation().subscribe(data=>{
      this.dataLocation = data.result.items;
    }), error => {};
  }

  loadJobSeeker(){
    this.dataService.getJobSeeker(this.filter).subscribe(data=>{
      this.data = data.result.items;
      this.total = data.result.totalCount;
    })
  }

  goToDetail(id){
    this.router.navigate(["detail-job-seeker/",id])
  }

  ngOnInit(): void {
    // generate random values for mainChart
   this.loadCategoryJob();
   this.loadLocation();
   this.loadJobSeeker();
  }
}
