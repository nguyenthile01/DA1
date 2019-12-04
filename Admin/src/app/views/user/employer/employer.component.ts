import { Component, OnInit } from "@angular/core";
import { EmployerService } from "./employer.service";
import { Employer } from "../../../app.model";
import { Router, ActivatedRoute } from "@angular/router";
import { BsModalService, BsModalRef } from "ngx-bootstrap/modal";
import { ModalDirective } from "ngx-bootstrap/modal";

@Component({
  selector: "app-employer",
  templateUrl: "./employer.component.html",
  styleUrls: ["./employer.component.css"]
})
export class EmployerComponent implements OnInit {
  public data: [];
  public total: 0;
  public detail: any = {};
  public openForm: boolean = false;
  public filter:  any={
    nameCompany: '',
    email: '',
    phoneNumber: '',
    page: 1,
    rowsPerPage: 10,
  };
  modalRef: BsModalRef;

  constructor(
    private dataService: EmployerService,
    private router: Router,
    private modalService: BsModalService,
    private route: ActivatedRoute,
  ) {
    this.route.params.subscribe(params => console.log(params));
  }

  loadData() {
    this.dataService.getEmployer(this.filter).subscribe(data => {
      this.data = data.result.items;
      this.total = data.result.totalCount;
    })
  }

  deleteItem(id) {
    this.dataService.delete(id).subscribe(data => {
      this.loadData();
    }),(error => {
      alert("Error: "+error.message)
    });
    
  }
  handleCreate() {
    this.router.navigate(["user/employer/", '']);
  }
  handleEdit(id) {
    this.router.navigate(["user/employer/", id]);
  }
  pageChanged(event: any): void {
    this.filter.page = event.page;
    this.loadData();
  }
  
  ngOnInit() {
    this.loadData();
  }
}
