import { Component, OnInit } from '@angular/core';
import { CityService } from './city.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-city',
  templateUrl: './city.component.html',
  styleUrls: ['./city.component.css']
})
export class CityComponent implements OnInit {
  public data: Array<any> =[];
  public total: 0;
  public filter: any = {
    name: "",
    page: 1,
    rowsPerPage: 10,
  }
  constructor(
    private dataService: CityService,
    private router: Router
  ) { }
    loadData(){
      this.dataService.getCity(this.filter).subscribe(data=>{
        this.data = data.result.items;
        this.total = data.result.totalCount;
      })
    }
    deleteItem(id){
      this.dataService.deleteItem(id).subscribe(data=>{
        alert("Delete sucess!")
      }),(error=>{
        alert("Error:"+error.response.data.error.message)
      })
    }
    handleEdit(id){
      this.router.navigate(["position/",id])
    }
    handleCreate(){
      this.router.navigate(["position/",""])
    }
  ngOnInit() {
    this.loadData()
  }

}
