import { Component, OnInit } from "@angular/core";
import { JobSeekerService } from "./job-seeker.service";
import { Router, ActivatedRoute } from "@angular/router";
import { BsModalService, BsModalRef } from "ngx-bootstrap/modal";
import { ModalDirective } from "ngx-bootstrap/modal";

@Component({
  selector: "app-job-seeker",
  templateUrl: "./job-seeker.component.html",
  styleUrls: ["./job-seeker.component.css"]
})
export class JobSeekerComponent implements OnInit {
  public filter: any = {
    name: "",
    JobLocationId: "",
    jobCategoryId: "",
    page: 1,
    rowsPerPage: 10
  };
  public data: [];
  public total: 0;

  constructor(private dataService: JobSeekerService, private router: Router) {}
  loadData() {
    this.dataService.getAll(this.filter).subscribe(data => {
      this.data = data.result.items;
      this.total = data.result.totalCount;
    });
  }
  pageChanged(event: any): void {
    this.filter.page = event.page;
    this.loadData();
  }
  deleteItem(id) {
    this.dataService.delete(id).subscribe(data => {
      this.loadData();
    }),
      (error => {
        error => error.response.data.error.message;
      });
  }
  handleCreate() {
    this.router.navigate(["user/jobSeeker", ""]);
  }
  handleEdit(id) {
    this.router.navigate(["user/jobSeeker", id]);
  }
  ngOnInit() {
    this.loadData();
  }
}
