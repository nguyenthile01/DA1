import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../category/category.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-category-detail',
  templateUrl: './category-detail.component.html',
  styleUrls: ['./category-detail.component.css']
})
export class CategoryDetailComponent implements OnInit {
  public data: any = {};
  private id: "";
  constructor(
    private dataService: CategoryService,
    private route: ActivatedRoute,
    private router: Router,
  ) {
    this.route.params.subscribe(params => {
      this.id = params.id;
    });
   }
   loadData(id){
    this.dataService.getForEdit(id).subscribe(data=>{
      this.data = data.result;
    })
   }
   createOrEdit(){
     this.dataService.createOrEdit(this.data).subscribe(data=>{
      if(this.id==''){
        alert("Create success!")
      }else{
        alert("Update success!")
      }
      this.router.navigate(["job/category"])
     })
   }
  ngOnInit() {
    this.loadData(this.id)
  }

}
