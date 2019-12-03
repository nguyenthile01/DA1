import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { IJob, ICity, IJobCategory } from "../../app.model";

@Injectable({
  providedIn: "root"
})
export class DetailJobService {
  constructor(private _http: HttpClient) {}

  getForEdit(id) {
    return this._http.get<IJob>(
      "http://localhost:21123/api/services/app/Job/GetForEdit",
      {
        params: { Id: id }
      }
    );
  }
  createOrUpdate(data) {
    return this._http.post<IJob>(
      "http://localhost:21123/api/services/app/Job/createOrUpdate",
      data
    );
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
}
