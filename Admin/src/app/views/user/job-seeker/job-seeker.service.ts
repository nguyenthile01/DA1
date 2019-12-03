import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import {
  Employer,
  IJobSeeker,
  ICity,
  IJobCategory,
  IDesiredCareer,
  IDesiredLocationJob,
  IKnowledge
} from "../../../app.model";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root"
})
export class JobSeekerService {
  constructor(private _http: HttpClient) {}

  getAll(filter) {
    return this._http.get<IJobSeeker>(
      "http://localhost:21123/api/services/app/JobSeeker/GetAll",
      { params: filter }
    );
  }
  getForEdit(id) {
    return this._http.get<IJobSeeker>(
      "http://localhost:21123/api/services/app/JobSeeker/GetForEdit",
      { params: { id: id } }
    );
  }
  createOrUpdate(data) {
    return this._http.post<IJobSeeker>(
      "http://localhost:21123/api/services/app/JobSeeker/CreateOrUpdate",
      data
    );
  }
  delete(id): Observable<any> {
    if (confirm("Do you want delete?")) {
      return this._http.delete(
        "http://localhost:21123/api/services/app/JobSeeker/Delete",
        { params: { Id: id } }
      );
    }
  }
  getCity() {
    return this._http.get<ICity>(
      "http://localhost:21123/api/services/app/City/GetAll"
    );
  }
  getJobCategory() {
    return this._http.get<IJobCategory>(
      "http://localhost:21123/api/services/app/JobCategory/GetAll"
    );
  }
  getDesiredCareer(filter) {
    return this._http.get<IDesiredCareer>(
      "http://localhost:21123/api/services/app/DesiredCareer/GetAll",
      {
        params: {
          JobSeekerId: filter
        }
      }
    );
  }
  getDesiredLocationJob(filter) {
    return this._http.get<IDesiredLocationJob>(
      "http://localhost:21123/api/services/app/DesiredLocationJob/GetAll",
      {
        params: {
          JobSeekerId: filter
        }
      }
    );
  }
  getKnowledge(filter) {
    return this._http.get<IDesiredLocationJob>(
      "http://localhost:21123/api/services/app/Knowledge/GetAll",
      {
        params: {
          JobSeekerId: filter
        }
      }
    );
  }
  getExperience(filter) {
    return this._http.get<IDesiredLocationJob>(
      "http://localhost:21123/api/services/app/Experience/GetAll",
      {
        params: {
          JobSeekerId: filter
        }
      }
    );
  }
  createOrUpdateKnowledge(data) {
    return this._http.post(
      "http://localhost:21123/api/services/app/Knowledge/CreateOrUpdate",
      data
    );
  }
  createOrUpdateExperience(data) {
    return this._http.post<IKnowledge>(
      "http://localhost:21123/api/services/app/Experience/CreateOrUpdate",
      data
    );
  }
}
