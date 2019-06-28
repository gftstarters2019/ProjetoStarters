import { Component, OnInit, Output, EventEmitter, Input, SimpleChanges } from '@angular/core';
import { FormControl, Validators, FormBuilder, FormGroup } from '@angular/forms';
import { GenericValidator } from '../Validations/GenericValidator';
import { BsDatepickerConfig} from 'ngx-bootstrap/datepicker';

export interface Color {
  value: string;
  name: string;
}

@Component({
  selector: 'app-beneficiary-vehicle',
  templateUrl: './beneficiary-vehicle.component.html',
  styleUrls: ['./beneficiary-vehicle.component.scss']
})
export class BeneficiaryVehicleComponent implements OnInit {

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
  
  vehicleCreateForm= this.formBuilder.group({
    vehicleBrand: new FormControl('', Validators.pattern(GenericValidator.regexSimpleName)),
    vehicleModel: new FormControl('', Validators.pattern(GenericValidator.regexSimpleName)),
    vehicleManufactoringYear: new FormControl('', GenericValidator.dateValidation()),
    vehicleModelYear: new FormControl('', GenericValidator.dateValidation()),
    vehicleColor: new FormControl('', Validators.required),
    vehicleChassisNumber: new FormControl('', Validators.pattern(GenericValidator.regexSimpleName)),
    vehicleCurrentMileage: new FormControl('', GenericValidator.negativeValidation()),
    vehicleCurrentFipeValue: new FormControl('', GenericValidator.negativeValidation()),
    vehicleDoneInspection: new FormControl(false)
  });



  constructor(private formBuilder: FormBuilder) {
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
