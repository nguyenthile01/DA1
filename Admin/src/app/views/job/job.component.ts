import { Component, OnInit } from '@angular/core';
import { JobService } from './job.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-job',
  templateUrl: './job.component.html',
  styleUrls: ['./job.component.css']
})
export class JobComponent implements OnInit {
  public data: []
  public total: 0
  filter: any = {
    title: "",
    page: 1,
    rowsPerPage: 10,
  }
  constructor(
    private dataService: JobService,
    private router: Router,
    private route: ActivatedRoute,
  ) { }
  loadData(){
    this.dataService.getAll(this.filter).subscribe(data=>{
      this.data = data.result.items;
      this.total = data.result.totalCount;
    }),(error=>{
      alert('error: '+error.response.data.error.message);
    })
  }
  deleteItem(id){
    this.dataService.deleteItem(id).subscribe(data=>{
      alert("Delete success!")
    }),(error=>{
      alert('error: '+error.response.data.error.message);
    })
  }
  handleCreate() {
    this.router.navigate(["job/detail-job/", '']);
  }
  handleEdit(id) {
    console.log(id);
    this.router.navigate(["job/detail-job/",id]);
    //this.router.navigate(["job/detail/", id]);
  }
  pageChanged(event: any): void {
    this.filter.page = event.page;
    this.loadData();
  }
  ngOnInit() {
    this.loadData();
  }

}
