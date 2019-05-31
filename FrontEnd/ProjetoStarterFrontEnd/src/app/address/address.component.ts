import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormBuilder, Validators, FormArray, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-address',
  templateUrl: './address.component.html',
  styleUrls: ['./address.component.scss']
})
export class AddressComponent implements OnInit {
  
  @Output() add = new EventEmitter<any>();
  @Input() address2: FormGroup;


  addressAdd: FormArray;

  address = this.fb.group ({
    street: ['', Validators.required],
    type: ['', Validators.required],
    number: ['', Validators.required],
    state: ['', Validators.required],
    neighborhood: [ '', Validators.required],
    country: ['', Validators.required],
    zipCode: ['', Validators.required],
    addressAdd: this.fb.array([])
  });


  constructor(private fb: FormBuilder) { }

  ngOnInit() {
  }

  public onSubmit(): void {
    debugger;

    console.log(this.address.value);
    this.add.emit(this.address.value);
  }

  createAddress(): FormGroup {
    return this.fb.group({
      id: ''
    });
  }

  addAddress(): void {
    this.addressAdd = this.address.get('addressAdd') as FormArray;
    if(this.addressAdd.length<5){
      this.addressAdd.push(this.createAddress());
    }
  }
  
}
