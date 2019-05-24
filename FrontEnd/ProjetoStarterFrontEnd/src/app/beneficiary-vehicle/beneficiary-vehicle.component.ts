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
    brand: new FormControl('', Validators.required),
    model: new FormControl('', Validators.required),
    manufactoringYear: new FormControl('', Validators.required),
    modelYear: new FormControl('', Validators.required),
    color: new FormControl('', Validators.required),
    chassisNumber: new FormControl('', Validators.required),
    currentMileage: new FormControl('', Validators.required),
    currentFipeValue: new FormControl('', Validators.required),
    doneInspection: new FormControl('', Validators.required)
  });

  constructor(private _httpClient: HttpClient, private formBuilder: FormBuilder) { }

  ngOnInit() {
  }

  public vehiclePost(): void{
    
    let form = JSON.stringify(this.vehicleCreateForm.value);
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })
    };
    this._httpClient.post(``, form, httpOptions)
    .subscribe(data => console.log(data));
  }

}
