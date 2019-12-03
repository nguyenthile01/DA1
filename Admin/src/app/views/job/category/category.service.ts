import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: "root"
})
export class CategoryService {
  constructor(private _http: HttpClient) {}
  getAll(filter) {
    return this._http.get(
      "http://localhost:21123/api/services/app/JobCategory/GetAll",
      { params: filter }
    );
  }
  getForEdit(id) {
    return this._http.get(
      "http://localhost:21123/api/services/app/JobCategory/GetForEdit",
      { params: { Id: id } }
    );
  }
  // createOrEdit(data)
}
