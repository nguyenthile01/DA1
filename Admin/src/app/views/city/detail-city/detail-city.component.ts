import { Component, OnInit } from "@angular/core";
import { CityService } from "../city.service";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
  selector: "app-detail-city",
  templateUrl: "./detail-city.component.html",
  styleUrls: ["./detail-city.component.css"]
})
export class DetailCityComponent implements OnInit {
  private id: "";
  public data: any = {};
  constructor(private dataService: CityService, private route: ActivatedRoute, private router: Router) {
    this.route.params.subscribe(params => {
      this.id = params.id;
    });
  }
  loadData(id) {
    this.dataService.getForEdit(id).subscribe(data => {
      this.data = data.result;
    });
  }
  createAndUpdateItem(data) {
    this.dataService.createOrEdit(data).subscribe(data => {
      if (this.id == "") {
        alert("Create success!");
      } else {
        alert("Update success!");
      }
      this.router.navigate(["/position"])
    }),
      error => {
        alert("error:" + error.response.data.error.message);
      };
  }
  ngOnInit() {
    this.loadData(this.id);
  }
}
