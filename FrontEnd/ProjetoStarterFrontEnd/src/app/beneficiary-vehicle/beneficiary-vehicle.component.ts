import { Component, OnInit } from '@angular/core';
import { FormControl, Validators, FormBuilder, FormGroup, AbstractControl } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { throwError } from 'rxjs';

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
    vehicleBrand: new FormControl('', this.isNaNValidation),
    vehicleModel: new FormControl('', Validators.required),
    vehicleManufactoringYear: new FormControl('', this.dateValidation),
    vehicleModelYear: new FormControl('', this.dateValidation),
    vehicleColor: new FormControl('', Validators.required),
    vehicleChassisNumber: new FormControl('', Validators.required),
    vehicleCurrentMileage: new FormControl('', this.negativeValidation),
    vehicleCurrentFipeValue: new FormControl('', this.negativeValidation),
    vehicleDoneInspection: new FormControl(false)
  });



  constructor(private _httpClient: HttpClient, private formBuilder: FormBuilder) {}

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
  public onSubmit(): void {
      console.log(this.vehicleCreateForm.value);
  }

  public isNaNValidation(control: AbstractControl): { [key: string]: boolean } | null{
    if(!isNaN(control.value))
      return {"EnteredANumber": true};
    
    return null;
  }

  public dateValidation(control: AbstractControl): { [key: string]: boolean } | null{
    if(control.value > Date.now())
      return {"EnteredADateHigherThanToday": true};
    
    return null;
  }

  public negativeValidation(control: AbstractControl): { [key: string]: boolean } | null{
    if(control.value < 0)
      return {"EnteredANegativeNumber": true};
    
    return null;
  }
}
