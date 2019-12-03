import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { IJob } from "../../app.model";

@Injectable({
  providedIn: "root"
})
export class JobService {
  constructor(private _http: HttpClient) {}
  getAll(filter) {
    return this._http.get<IJob>(
      "http://localhost:21123/api/services/app/Job/GetAll",
      { params: filter }
    );
  }
  getForEdit(id) {
    return this._http.get("http://localhost:21123/api/services/app/Job/GetAll");
  }
  deleteItem(id) {
    if (confirm("Do you want delete?")) {
      return this._http.delete<IJob>(
        "http://localhost:21123/api/services/app/Job/Delete",
        { params: { Id: id } }
      );
    }
  }
}
