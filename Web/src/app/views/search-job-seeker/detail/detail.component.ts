import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { JobSeekerService } from '../job-seeker.service';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.css']
})
export class DetailComponent implements OnInit {
  public data: any = {};
  public dataExperience: Array<any> = [];
  public dataDesiredLocation: any = [];
  public dataDesiredCareer: any = [];
  public dataKnowledge: Array<any> = [];
  public id: "";

  constructor(
    private route: ActivatedRoute,
    private dataService: JobSeekerService
  ) {
    this.route.params.subscribe( params => {
      this.id = params.id;
    } );
   }
  loadData(id){
    this.dataService.getForEdit(id).subscribe(data=>{
      this.data = data.result;
    })
  }
  loadExpeience(id){
    this.dataService.getExperience(id).subscribe(data=>{
      this.dataExperience = data.result.items;
    })
  }
  loadDesireCareeer(id){
    this.dataService.getDesiredCareer(id).subscribe(data=>{
      this.dataDesiredCareer = data.result.items;
    })
  }
  loadKnowledge(id){
    this.dataService.getKnowledge(id).subscribe(data=>{
      this.dataKnowledge = data.result.items;
    })
  }
  loadDesiredLocationJob(id){
    this.dataService.getDesiredLocationJob(id).subscribe(data=>{
      this.dataDesiredLocation = data.result.items;
    })
  }
  ngOnInit() {
    this.loadData(this.id);
    this.loadDesireCareeer(this.id);
    this.loadExpeience(this.id);
    this.loadKnowledge(this.id);
    this,this.loadDesiredLocationJob(this.id)
  }

}
