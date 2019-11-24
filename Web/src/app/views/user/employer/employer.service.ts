import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Employer } from "../user.model";

@Injectable({
  providedIn: "root"
})
export class EmployerService {
  constructor(private _http: HttpClient) {}
  getEmployer(filter) {
    return this._http.get<Employer>(
      "http://localhost:21123/api/services/app/Employer/GetAll",
      { params: filter }
    );
  }
}
