import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-address',
  templateUrl: './address.component.html',
  styleUrls: ['./address.component.scss']
})
export class AddressComponent implements OnInit {

  address = this.fb.group ({
    street: ['', Validators.required],
    type: ['', Validators.required],
    number: ['', Validators.required],
    state: ['', Validators.required],
    neighborhood: [ '', Validators.required],
    country: ['', Validators.required],
    zipCode: ['', Validators.required]
  });

  constructor(private fb: FormBuilder) { }

  ngOnInit() {
  }

  public onSubmit(): void {
    console.log(this.address.value);
  }

}
