import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { Cities } from './city.model';
import { DataService } from './data.service';
import {freeApiSevices} from './views/freeapi';

@Component({
  // tslint:disable-next-line
  selector: 'body',
  template: '<router-outlet></router-outlet>'
})
export class AppComponent implements OnInit {
  
  constructor(private router: Router) { }

  ngOnInit() {
    this.router.events.subscribe((evt) => {
      if (!(evt instanceof NavigationEnd)) {
        return;
      }
      window.scrollTo(0, 0);
    });
  }
}
export class App implements OnInit{
  constructor(private _freeApiSevices: freeApiSevices){}

  lstCities: Cities [];

  ngOnInit(){
    this._freeApiSevices.getCity()
    .subscribe
    (
      data=>
      {
        this.lstCities = data.data.result.items;
      }
    );
  }
}


