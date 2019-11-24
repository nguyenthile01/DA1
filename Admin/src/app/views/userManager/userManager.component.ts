import { Component, OnInit, enableProdMode } from '@angular/core';
import { getStyle, hexToRgba } from '@coreui/coreui/dist/js/coreui-utilities';
import { CustomTooltips } from '@coreui/coreui-plugin-chartjs-custom-tooltips';
import { UserManagerService } from './userManager.service';

@Component({
  template:`
  <h2>Xin chào</h2>
  `
})
export class UserManagerComponent implements OnInit {
  // public cities: ICities[];
  public cities: [];
  public total: 0;

//   constructor(private _dashboadrService: UserManagerService){};

  enableProdMode(): void{};
  ngOnInit(): void {
    // this._dashboadrService.getCities().subscribe(data=>{
    //   this.cities = data.result.items;
    //   this.total = data.result.totalCount;
    // });
  }
}
