import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { ICategoryJob, ILocation, IJobSeeker } from "./job-seeker.model";

@Injectable({
  providedIn: "root"
})
export class JobSeekerService {
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

  getForEdit(id){
    return this.http.get<IJobSeeker>(
        "http://localhost:21123/api/services/app/JobSeeker/GetForEdit",
        { params: {Id: id} }
      );
  }
  getExperience(id){
    return this.http.get<IJobSeeker>(
        "http://localhost:21123/api/services/app/Experience/GetAll",
        { params: {JobSeekerId: id} }
      );
  }
  getKnowledge(id){
    return this.http.get<IJobSeeker>(
        "http://localhost:21123/api/services/app/Knowledge/GetAll",
        { params: {JobSeekerId: id} }
      );
  }
  getDesiredCareer(id){
    return this.http.get<IJobSeeker>(
        "http://localhost:21123/api/services/app/DesiredCareer/GetAll",
        { params: {JobSeekerId: id} }
      );
  }
  getDesiredLocationJob(id){
    return this.http.get<IJobSeeker>(
        "http://localhost:21123/api/services/app/DesiredLocationJob/GetAll",
        { params: {JobSeekerId: id} }
      );
  }
}
