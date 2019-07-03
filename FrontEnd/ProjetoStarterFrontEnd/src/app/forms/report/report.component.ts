import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { FormControl, FormGroup } from '@angular/forms';
import { MatAutocompleteSelectedEvent } from '@angular/material';

export interface Holder {
  individualId: string;
  individualBirthdate: string;
  individualCPF: string;
  individualEmail: string;
  individualName: string;
  individualRG: string;
}


@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.scss']
})
export class ReportComponent implements OnInit {

  disableInput: boolean = false;
  FilteredHolder$: Observable<Holder[]>
  control_autocomplete = new FormControl();
  reportform: FormGroup;
  constructor() { }

  ngOnInit() {
  }

  contractHolder_namecompleteSelect(event: MatAutocompleteSelectedEvent){
    const holder: Holder = event.option.value;
    this.reportform.get('contractHolderId').setValue(holder.individualId);
  }

}
