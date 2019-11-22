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
// export class AppComponent implements OnInit {
  
//   constructor(private router: Router) { }

//   ngOnInit() {
//     this.router.events.subscribe((evt) => {
//       if (!(evt instanceof NavigationEnd)) {
//         return;
//       }
//       window.scrollTo(0, 0);
//     });
//   }
// }
export class AppComponent implements OnInit{
  lstCities: Cities [];
  constructor(private dataService: DataService){}

  ngOnInit(){
    return this.dataService.getCities().subscribe(data=>this.lstCities=data);
    console.log(this.lstCities);
  }
}


