import { Component, OnInit } from '@angular/core';
import { FormControl, Validators, FormBuilder } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';

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
  
  vehicleCreateForm= this.formBuilder.group({
    vehicleBrand: new FormControl('', Validators.required),
    vehicleModel: new FormControl('', Validators.required),
    vehicleManufactoringYear: new FormControl('', Validators.required),
    vehicleModelYear: new FormControl('', Validators.required),
    vehicleColor: new FormControl('', Validators.required),
    vehicleChassisNumber: new FormControl('', Validators.required),
    vehicleCurrentMileage: new FormControl('', Validators.required),
    vehicleCurrentFipeValue: new FormControl('', Validators.required),
    vehicleDoneInspection: new FormControl(false)
  });

  constructor(private _httpClient: HttpClient, private formBuilder: FormBuilder) { }

  ngOnInit() {
  }

  response:Object;

  public vehiclePost(): void{
    
    let form = JSON.stringify(this.vehicleCreateForm.value);
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })
    };
    this._httpClient.post('https://beneficiarieswebapi.azurewebsites.net/api/Beneficiary/Vehicle', form, httpOptions)
    .subscribe(data => {this.response = data});
    
  }

}
