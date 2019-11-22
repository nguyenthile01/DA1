import{Injectable} from '@angular/core';
import { Observable } from 'rxjs';
import {HttpClient} from '@angular/common/http'

@Injectable()
export class freeApiSevices{

    constructor(private httpclient: HttpClient){}
    getCity(): Observable<any>{
        return this.httpclient.get('http://localhost:21123/api/services/app/City/GetAll');
    }
}