import { Injectable } from "@angular/core";
import { IEmployer } from "../employer.model";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: "root"
})
export class CreateEmployersService {
  constructor(private _http: HttpClient) {}

  createEmployer(filter) {
    return this._http.post<IEmployer>(
      "http://localhost:21123/api/services/app/Employer/CreateOrUpdate",
      filter
    );
  }
}
