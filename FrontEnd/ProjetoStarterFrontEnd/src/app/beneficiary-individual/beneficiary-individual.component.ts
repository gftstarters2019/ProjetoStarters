import { Component, OnInit, Output, EventEmitter, Input, SimpleChanges } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { GenericValidator } from '../Validations/GenericValidator';

@Component({
  selector: 'app-beneficiary-individual',
  templateUrl: './beneficiary-individual.component.html',
  styleUrls: ['./beneficiary-individual.component.scss']
})
export class BeneficiaryIndividualComponent implements OnInit {

  @Input() individualForm: FormGroup;

  @Input() individualPushPermission !: number;

  @Output() messageIndividualEvent = new EventEmitter<any>();

  public cpfMask = [/\d/, /\d/, /\d/, '.', /\d/, /\d/, /\d/, '.', /\d/, /\d/, /\d/, '-', /\d/, /\d/];
  public rgMask= [/\d/, /\d/, '.', /\d/, /\d/, /\d/, '.', /\d/, /\d/, /\d/, '-', /[X0-9]/]

  individualCreateForm= this.formBuilder.group({
    individualName: ['', Validators.pattern(GenericValidator.regexName)],
    individualCPF: ['', GenericValidator.isValidCpf()],
    individualRG: ['', GenericValidator.rgLengthValidation()],
    individualBirthdate: ['', GenericValidator.dateValidation()],
    individualEmail: ['', Validators.required]
  });

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit() {
    
  }

  ngOnChanges(changes: SimpleChanges) {
    if(changes.individualPushPermission.currentValue != 0 && changes.individualPushPermission.currentValue != changes.individualPushPermission.previousValue) {
      let cpf = this.individualCreateForm.get('individualCPF').value;
      cpf = cpf.replace(/\D+/g, '');
      this.individualCreateForm.get('individualCPF').setValue(cpf);

      let rg = this.individualCreateForm.get('individualRG').value;
      rg = rg.replace(/\D+/g, '');
      this.individualCreateForm.get('individualRG').setValue(rg);
      this.messageIndividualEvent.emit(this.individualCreateForm);
    }
  }
}