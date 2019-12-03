import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Employer, IJobSeeker } from "../../../app.model";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root"
})
export class EmployerService {
  constructor(private _http: HttpClient) {}
  getEmployer(filter) {
    console.log(filter);
    return this._http.get<Employer>(
      "http://localhost:21123/api/services/app/Employer/GetAll",
      { params: filter }
    );
  }
  getForEdit(id) {
    return this._http.get<Employer>(
      "http://localhost:21123/api/services/app/Employer/GetForEdit",
      { params: { id: id } }
    );
  }
  createOrUpdate(data) {
    return this._http.post<Employer>(
      "http://localhost:21123/api/services/app/Employer/CreateOrUpdate",
      data
    );
  }
  delete(id): Observable<any> {
    if (confirm("Do you want delete?")) {
      return this._http.delete(
        "http://localhost:21123/api/services/app/Employer/Delete",
        { params: { Id: id } }
      );
    }
  }
}
