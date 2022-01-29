import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormBuilder,  Validators, FormArray} from '@angular/forms';
//import { forbiddenNameValidator } from '../directives/directiveForbidden';


@Component({
  selector: 'app-form-test',
  templateUrl: './form-test.component.html',
  styleUrls: ['./form-test.component.css']
})
export class FormTestComponent implements OnInit {

  name = new FormControl('');
  cursForm = new FormGroup({
    name: new FormControl('', [
      Validators.required,
      Validators.minLength(4)//, forbiddenNameValidator(/bob/i) // <-- Here's how you pass in the custom validator.
    ]),
    alterEgo: new FormControl(''),
    power: new FormControl('', Validators.required)
  });

  profileForm = this.fb.group({
    firstName: ['', Validators.required, Validators.minLength(4)], //forbiddenNameValidator(/bob/1)],
    lastName: [''],
    address: this.fb.group({
      street: [''],
      city: [''],
      state: [''],
      zip: ['']
    }),
    aliases: this.fb.array([
      this.fb.control('')
    ])
  });

  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
  }

  get aliases() {
    return this.profileForm.get('aliases') as FormArray;
  }
  
  addAlias() {
    this.aliases.push(this.fb.control(''));
  }

  updateName() {
    this.name.setValue('Nancy');
  }

  onSubmit() {
    // TODO: Use EventEmitter with form value
    console.warn(this.profileForm.value);
  }

  updateProfile() {
    this.profileForm.patchValue({
      firstName: 'Nancy',
      address: {
        street: '123 Drew Street'
      }
    });
  }
}
