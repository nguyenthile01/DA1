import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { DetailJobService } from "./detail-job.service";

@Component({
  selector: "app-detail-job",
  templateUrl: "./detail-job.component.html",
  styleUrls: ["./detail-job.component.css"]
})
export class DetailJobComponent implements OnInit {
  private id: "";
  public deadlineForSubmission = new Date();
  public data: any = {};
  public city: Array<any> = [];
  public category: Array<any> = [];
  public rankAtWork: Array<any> = [
    "Just graduated / Trainee",
    "Employees",
    "Leader",
    "Vice president",
    "Manager",
    "Director-General",
    "Others"
  ];
  public experience: Array<any> = [
    "No experience yet",
    "Less than 1 year",
    "1 year",
    "2 years",
    "3 years",
    "4 years",
    "5 years or more",
  ]
  public degree: Array<any> = [
    "Unskilled labor",
    "High school",
    "Intermediate",
    "College",
    "University",
    "Graduate",
    "Others"
  ]
  public gender: Array<any> = [
    "Male",
    "Female"
  ];
  public language: Array<any> = [
    "Vietnamese",
    "English"
  ];
  public minDate: Date;
  public maxDate: Date;

  constructor(
    private dataSevice: DetailJobService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.route.params.subscribe(params => {
      this.id = params.id;
    });
    this.minDate = new Date();
    this.maxDate = new Date();
    this.minDate.setDate(this.minDate.getDate());
    this.maxDate.setDate(this.maxDate.getDate() + 90);
  }
  loadCity() {
    this.dataSevice.getCity().subscribe(data => {
      this.city = data.result.items;
    });
  }
  
  loadJobCategory() {
    this.dataSevice.getJobCategory().subscribe(data => {
      this.category = data.result.items;
    });
  }
  loadData(id) {
    this.dataSevice.getForEdit(id).subscribe(data => {
      this.data = data.result;
    });
  }
  handleSave(data){
    this.dataSevice.createOrUpdate(data).subscribe(data=>{
      if(this.id==''){
        alert("Create success!")
      }else{
        alert("Update success!")
      }
      this.router.navigate(["job/post"])
    }),(error=>{
      alert("error:" + error.response.data.error.message)
    })
  }
  ngOnInit() {
    this.loadCity();
    this.loadJobCategory();
    this.loadData(this.id);
  }
}
