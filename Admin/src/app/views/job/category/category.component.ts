import { Component, OnInit } from '@angular/core';
import { CategoryService } from './category.service';
import { error } from '@angular/compiler/src/util';
import { Router } from '@angular/router';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {

  public data: Array<any> = [];
  public total: 0;
  public filter: any = {
    name: "",
    isActive: "",
  }
  constructor(
   private dataService: CategoryService,
   private router: Router
  ) { }
    loadData(){
      this.dataService.getAll(this.filter).subscribe(data=>{
        this.data = data.result.items;
        this.total = data.result.totalCount;
      })
    }
    handleEdit(id){
      this.router.navigate(["job/detail-category/",id])
    }
    handleCreate(){
      this.router.navigate(["job/detail-category/",""])
    }
    deleteItem(id){
      this.dataService.deleteItem(id).subscribe(data=>{
        alert("Delete succes!");
      }),(error=>{
        alert("Error: "+error.response.data.error.message);
      })
    }
  ngOnInit() {
    this.loadData();
  }

}
