import {Injectable, Inject} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';

export interface ICities {
  id: number;
  name: string;
  nameCompany: string;
  userName: string;
  emailAddress: string;
  phoneNumber: string;
  password: string;
  address: string;
  result: Object;
  }

  @Injectable({
    providedIn: 'root'
  })

export class DashboarService {
    constructor(private http: HttpClient){}
    getCities(): Observable<ICities[]>{
        return this.http.get<ICities[]>('http://localhost:21123/api/services/app/Employer/GetAll')
    }
}

