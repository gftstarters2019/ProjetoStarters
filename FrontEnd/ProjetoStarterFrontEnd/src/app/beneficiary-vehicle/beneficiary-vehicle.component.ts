import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormControl, Validators, FormBuilder } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { throwError } from 'rxjs';
import { GenericValidator } from '../Validations/GenericValidator';

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



  constructor(private _httpClient: HttpClient, private formBuilder: FormBuilder) {}

  ngOnInit() {
  }

  response:any;

  public vehiclePost(): void{
    
    let form = JSON.stringify(this.vehicleCreateForm.value);
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })
    };
    this._httpClient.post('https://beneficiarieswebapi.azurewebsites.net/api/Beneficiary/Vehicle', form, httpOptions)
    .subscribe(data => {
      this.response = data;
      if(this.response != null){
        this.messageVehicleEvent.emit(this.response.beneficiaryId);
      }
    });
  }
}
