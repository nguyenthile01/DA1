import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IEmployer } from '../employer.model';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(
    private _http: HttpClient,
  ) { }

  loginEmployer(username, password){
    return this._http.get<IEmployer>(
      "http://localhost:21123/api/services/app/Employer/GetForLogin",
      {params: {
        userName: username,
        password: password,
      }}
    )
  }
}
