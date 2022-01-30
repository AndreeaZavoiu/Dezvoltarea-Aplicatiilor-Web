import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../auth.service';
import { Register } from '../models/register';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

registrationForm = this.fb.group({
  email : ['', Validators.required],
  password : ['']

});
  constructor(private fb: FormBuilder, private authService : AuthService ) { }

  ngOnInit(): void {
  }

onRegister () {
var newUser = new Register(this.registrationForm.value)
this.authService.register(newUser).subscribe();
}

}

