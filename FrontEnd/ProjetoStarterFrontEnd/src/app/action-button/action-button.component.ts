import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-action-button',
  templateUrl: './action-button.component.html',
  styleUrls: ['./action-button.component.scss']
})
export class ActionButtonComponent implements OnInit  {
  private params:any;

  agInit(params:any):void {
      this.params = params;
  }

  private valueSquared():number {
      return this.params.value * this.params.value;
  }
  constructor() { }

  ngOnInit() {
  }

}
