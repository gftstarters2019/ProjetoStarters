import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';

export interface Address{
  id : string,
  street: string,
  type: string,
  number: number,
  state: string,
  neighborhood: string,
  country: string,
  zipCode: string
}

@Component({
  selector: 'app-address',
  templateUrl: './address.component.html',
  styleUrls: ['./address.component.scss']
})
export class AddressComponent implements OnInit {

  @Output() createEvent = new EventEmitter<FormGroup>();

  address = this.fb.group ({
    id: [''],
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
    this.createEvent.emit(this.address);
  }

}
