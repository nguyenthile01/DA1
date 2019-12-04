import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ICity } from '../../app.model';


@Injectable({
  providedIn: "root"
})
export class CityService {
  constructor(private _http: HttpClient) {}

  getCity(filter) {
    return this._http.get<ICity>(
      "http://localhost:21123/api/services/app/City/GetAll",
      { params: filter }
    );
  }
  deleteItem(id) {
    if (confirm("Do you want delete?")) {
      return this._http.delete<ICity>(
        "http://localhost:21123/api/services/app/City/Delete",
        { params: { Id: id } }
      );
    }
  }
  getForEdit(id) {
    return this._http.get<ICity>(
      "http://localhost:21123/api/services/app/City/GetForEdit",
      { params: { Id: id } }
    );
  }
  createOrEdit(data) {
    return this._http.post<ICity>(
      "http://localhost:21123/api/services/app/City/CreateOrUpdate",
      data
    );
  }
}
