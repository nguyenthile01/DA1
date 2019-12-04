import { Component, OnInit } from "@angular/core";
import { JobSeekerService } from "../job-seeker.service";
import { Route } from '@angular/compiler/src/core';
import { ActivatedRoute } from '@angular/router';
import { ThemeService } from 'ng2-charts';

@Component({
  selector: "app-detail-jobseeker",
  templateUrl: "./detail-jobseeker.component.html",
  styleUrls: ["./detail-jobseeker.component.css"]
})
export class DetailJobseekerComponent implements OnInit {
  public data: any = {};
  private id: "";
  public city: Array<any> = [];
  public jobCategory: Array<any> = [];
  public experience: Array<any> = [];
  public knowledge: Array<any> = [];
  public desiredLocation: Array<any> = [];
  public desiredJobCategory: Array<any> = [];
  public date: Date = new Date("DD/MM/YYYY");
  public jobSeekerId: "";
  constructor(private dataService: JobSeekerService,
    private route: ActivatedRoute
    ) {
      this.route.params.subscribe( params => {
        this.id = params.id;
        this.jobSeekerId = params.id;
      } );
    }
  getForEdit(id) {
    this.dataService.getForEdit(id).subscribe(data => {
      this.data = data.result;
    });
  }
  getCity(){
    this.dataService.getCity().subscribe(data=>{
      this.city = data.result.items;
    })
  }
  getJobCategory(){
    this.dataService.getJobCategory().subscribe(data=>{
      this.jobCategory = data.result.items;
    })
  }
  getDesiredLocation(filter){
    this.dataService.getDesiredCareer(filter).subscribe(data=>{
      this.desiredLocation = data.result.items;
    })
  }
  getDesiredJobCategory(filter){
    this.dataService.getDesiredLocationJob(filter).subscribe(data=>{
      this.desiredJobCategory = data.result.items;
    })
  }
  getKnowledge(filter){
    this.dataService.getKnowledge(filter).subscribe(data=>{
      this.knowledge = data.result.items;
    })
  }
  getExperience(filter){
    this.dataService.getExperience(filter).subscribe(data=>{
      this.experience = data.result.items;
    })
  }
  handlecreateOrUpdate(data){
    if(data!=null){
      this.dataService.createOrUpdate(data).subscribe(data=>{
        if(this.id == ''){
          alert("Create success!")
        }
        else{
          alert("Update success!")
        }
      }),(error=>{
        alert("Fail!")
      })
    }
  }
  handleCreateOrEditKnowledge(data){
    if(data!=null){
      this.dataService.createOrUpdateKnowledge(data).subscribe(data=>{
        if(this.id == ''){
          alert("Create success!")
        }
        else{
          alert("Update success!")
        }
      }),(error=>{
        alert("Fail!")
      })
    }
  }
  handleCreateOrEditExperience(data){
    if(data!=null){
      this.dataService.createOrUpdateExperience(data).subscribe(data=>{
        if(this.id == ''){
          alert("Create success!")
        }
        else{
          alert("Update success!")
        }
      }),(error=>{
        alert("Fail!")
      })
    }
  }
  ngOnInit() {
    this.getForEdit(this.id);
    this.getCity();
    this.getJobCategory();
    this.getKnowledge(this.jobSeekerId);
    this.getExperience(this.jobSeekerId);
    this.getDesiredJobCategory(this.jobSeekerId);
    this.getDesiredLocation(this.jobSeekerId)
  }
}
