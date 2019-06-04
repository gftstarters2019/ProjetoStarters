import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormBuilder, Validators, FormGroup, AbstractControl } from '@angular/forms';

export interface Telephone{
  id: string,
  telephoneNumber: string,
  telephoneType: string
}

@Component({
  selector: 'app-telephone',
  templateUrl: './telephone.component.html',
  styleUrls: ['./telephone.component.scss']
})


export class TelephoneComponent implements OnInit {
  message:string;
  telephone = this.fb.group ({
    id: [''],
    telephoneNumber: ['', [Validators.pattern(/^[0-9]+$/), Validators.maxLength(11), Validators.minLength(10)]],
    telephoneType: ['', Validators.required]
  });
  @Output () addTelephone = new EventEmitter<any>();
  @Input() telephone2: FormGroup;
  constructor(private fb: FormBuilder) { }

  ngOnInit() {
  }

  public onSubmit(): void {
    this.message=this.telephone.get(['id']).value;
    this.addTelephone.emit(this.telephone.value);
  }

  emitValueTelphone() {
    this.addTelephone.emit(this.telephone.value)
  }
}
