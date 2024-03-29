import { Injectable } from '@angular/core';

import { Cities } from './city.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  apiUrl = 'http://localhost:21123/api/services/app/City/GetAll';
  constructor(private _http: HttpClient) { }

  getCities()
  {
    return this._http.get<Cities[]>(this.apiUrl);    
  }
}
