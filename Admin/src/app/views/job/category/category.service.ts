import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { IJobCategory } from "../../../app.model";

@Injectable({
  providedIn: "root"
})
export class CategoryService {
  constructor(private _http: HttpClient) {}
  getAll(filter) {
    return this._http.get<IJobCategory>(
      "http://localhost:21123/api/services/app/JobCategory/GetAll",
      { params: filter }
    );
  }
  getForEdit(id) {
    return this._http.get<IJobCategory>(
      "http://localhost:21123/api/services/app/JobCategory/GetForEdit",
      { params: { Id: id } }
    );
  }
  createOrEdit(data) {
    return this._http.post<IJobCategory>(
      "http://localhost:21123/api/services/app/JobCategory/CreateOrUpdate",
      data
    );
  }
  deleteItem(id) {
    if (confirm("Do you want delete?")) {
      return this._http.delete(
        "http://localhost:21123/api/services/app/JobCategory/Delete",
        { params: { Id: id } }
      );
    }
  }
}
