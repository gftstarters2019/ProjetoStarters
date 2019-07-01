import { Component, OnInit, Input, Output, EventEmitter, SimpleChanges } from '@angular/core';
import { BsDatepickerConfig} from 'ngx-bootstrap/datepicker';
import { FormGroup, FormBuilder } from '@angular/forms';

export interface Species {
  value: string;
  name:string;
}

@Component({
  selector: 'app-pets',
  templateUrl: './pets.component.html',
  styleUrls: ['./pets.component.scss']
})
export class PetsComponent implements OnInit {

  @Input() petForm: FormGroup;

  @Input() petPushPermission !: number;

  @Output() messagePetEvent = new EventEmitter<any>();
  bsConfig: Partial<BsDatepickerConfig>;


  constructor(private fb: FormBuilder) {
    this.bsConfig = Object.assign({}, {containerClass: 'theme-dark-blue'});
   }

  ngOnInit() {
  }

  ngOnChanges(changes: SimpleChanges) {
    if(changes.petPushPermission.currentValue != 0 && changes.petPushPermission.currentValue != changes.petPushPermission.previousValue) {
      // let birthdate = this.petForm.get('petBirthdate').value;
      // this.petForm.get('petBirthdate').setValue(new Date(birthdate));
      this.messagePetEvent.emit(this.petForm);

    }
  }
}
