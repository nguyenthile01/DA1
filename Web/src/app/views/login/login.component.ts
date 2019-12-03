import { Component, OnInit } from '@angular/core';
import { LoginService } from './login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: 'login.component.html'
})
export class LoginComponent implements OnInit{
  public filter: any = {
    userName: '',
    password: ''
  } 
  constructor(
    private dataService: LoginService,
    private router: Router
  ){}
  loginEmployer(){
    this.dataService.loginEmployer(this.filter.userName, this.filter.password).subscribe(data=>{
      this.router.navigate(["dashboard"]);
    })
  }
  ngOnInit(){

  }
 }
