import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { ICategoryJob, ILocation, IJobSeeker } from "./dashboard.model";

@Injectable({
  providedIn: "root"
})
export class DashboarService {
  constructor(private http: HttpClient) {}

  getCategoryJob() {
    return this.http.get<ICategoryJob>(
      "http://localhost:21123/api/services/app/JobCategory/GetAll"
    );
  }

  getLocation() {
    return this.http.get<ILocation>(
      "http://localhost:21123/api/services/app/City/GetAll"
    );
  }

  getJobSeeker(filter) {
    return this.http.get<IJobSeeker>(
      "http://localhost:21123/api/services/app/JobSeeker/GetAll",
      { params: filter }
    );
  }
}
