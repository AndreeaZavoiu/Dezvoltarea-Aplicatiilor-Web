import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../auth.service';
import { Login } from '../models/login';
import { Register } from '../models/register';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

loginForm = this.fb.group({
  email : ['', Validators.required],
  password : ['']

});
  constructor(private fb: FormBuilder, private authService : AuthService ) { }

  ngOnInit(): void {
  }

onLogin () {
var user = new Login(this.loginForm.value);
this.authService.login(user).subscribe();
}

}

