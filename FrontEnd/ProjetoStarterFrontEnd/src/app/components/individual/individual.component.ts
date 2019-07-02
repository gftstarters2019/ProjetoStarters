import { Component, OnInit, Input, Output, EventEmitter, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';

@Component({
  selector: 'app-individual',
  templateUrl: './individual.component.html',
  styleUrls: ['./individual.component.scss']
})
export class IndividualComponent implements OnInit {

  @Input() individualForm: FormGroup;

  @Input() individualPushPermission !: number;

  @Output() messageIndividualEvent = new EventEmitter<any>();

  bsConfig: Partial<BsDatepickerConfig>;

  public cpfMask = [/\d/, /\d/, /\d/, '.', /\d/, /\d/, /\d/, '.', /\d/, /\d/, /\d/, '-', /\d/, /\d/];
  public rgMask= [/\d/, /\d/, '.', /\d/, /\d/, /\d/, '.', /\d/, /\d/, /\d/, '-', /[X0-9]/]
  
  constructor(private fb: FormBuilder) {
    this.bsConfig = Object.assign({}, { containerClass: 'theme-dark-blue' });

  }

  ngOnInit() {

  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes.individualPushPermission.currentValue != 0 && changes.individualPushPermission.currentValue != changes.individualPushPermission.previousValue) {

      let cpf = this.individualForm.get('individualCPF').value;
      cpf = cpf.replace(/\D+/g, '');
      this.individualForm.get('individualCPF').setValue(cpf);

      let rg = this.individualForm.get('individualRG').value;
      rg = rg.replace(/\D+/g, '');
      this.individualForm.get('individualRG').setValue(rg);

      this.messageIndividualEvent.emit(this.individualForm);


    }
  }
}
