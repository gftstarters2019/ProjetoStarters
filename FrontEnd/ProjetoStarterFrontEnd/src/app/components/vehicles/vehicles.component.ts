import { Component, OnInit, Output, EventEmitter, Input, SimpleChanges } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { BsDatepickerConfig} from 'ngx-bootstrap/datepicker';

export interface Color {
  value: string;
  name: string;
}

@Component({
  selector: 'app-vehicles',
  templateUrl: './vehicles.component.html',
  styleUrls: ['./vehicles.component.scss']
})
export class VehiclesComponent implements OnInit {

  bsConfig: Partial<BsDatepickerConfig>;
  colors: Color[] = [
    {value: '0', name: 'White'},
    {value: '1', name: 'Silver'},
    {value: '2', name: 'Black'},
    {value: '3', name: 'Gray'},
    {value: '4', name: 'Red'},
    {value: '5', name: 'Blue'},
    {value: '6', name: 'Brown'},
    {value: '7', name: 'Yellow'},
    {value: '8', name: 'Green'},
    {value: '9', name: 'Other'}
  ];

  @Input() vehicleForm: FormGroup;

  @Input() vehiclePushPermission !: number;

  @Output() messageVehicleEvent = new EventEmitter<any>();

  constructor() {
    this.bsConfig = Object.assign({}, {containerClass: 'theme-dark-blue'});
  }

  ngOnInit() {
  }

  ngOnChanges(changes: SimpleChanges) {
    if(changes.vehiclePushPermission.currentValue != 0 && changes.vehiclePushPermission.currentValue != changes.vehiclePushPermission.previousValue) {
      
      this.messageVehicleEvent.emit(this.vehicleForm);
    }
  }
}
