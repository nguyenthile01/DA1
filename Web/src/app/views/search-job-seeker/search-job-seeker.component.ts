import { Component, OnInit } from "@angular/core";
import { JobSeekerService } from "./job-seeker.service";
import { Route } from "@angular/compiler/src/core";
import { Router } from "@angular/router";

@Component({
  selector: "app-search-job-seeker",
  templateUrl: "./search-job-seeker.component.html",
  styleUrls: ["./search-job-seeker.component.scss"]
})
export class SearchJobSeekerComponent implements OnInit {
  public filter: any = {
    name: "",
    jobLocationId: "",
    categoryId: "",
    YearsOfExperience: ""
  };
  public dataCategory: [];
  public dataLocation: [];
  public data: [];
  public total: 0;

  constructor(private dataService: JobSeekerService, private router: Router) {}

  loadCategoryJob() {
    this.dataService.getCategoryJob().subscribe(data => {
      this.dataCategory = data.result.items;
    }),
      error => {};
  }

  loadLocation() {
    this.dataService.getLocation().subscribe(data => {
      this.dataLocation = data.result.items;
    }),
      error => {};
  }

  loadJobSeeker() {
    this.dataService.getJobSeeker(this.filter).subscribe(data => {
      this.data = data.result.items;
      this.total = data.result.totalCount;
    });
  }

  goToDetail(id) {
    this.router.navigate(["search/job-seeker/", id]);
  }

  ngOnInit() {
    this.loadCategoryJob();
    this.loadLocation();
    this.loadJobSeeker();
  }
}
