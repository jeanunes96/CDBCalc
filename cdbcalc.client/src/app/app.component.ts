import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

interface CalculationRequest {
  initialValue: number;
  totalMonth: number;  
}

interface CalculationResponse {
  initialValue?: number;  
  totalMonths: number;    
  grossAmount?: number;   
  taxAmount?: number;     
  netAmount?: number;     
  cdi?: number;           
  bankRate?: number; 
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  public response: CalculationResponse | null = null;
  public errorMessage: string | null = null; 
  public cdbForm: FormGroup; 

  constructor(private http: HttpClient, private fb: FormBuilder) {
    this.cdbForm = this.fb.group({
      valor: [0, [Validators.required, Validators.min(0)]],
      mes: [0, [Validators.required, Validators.min(1)]],
    });
  }

  ngOnInit() {}

  calculateCdb() {
    if (this.cdbForm.valid) {
      const request: CalculationRequest = {
        initialValue: this.cdbForm.value.valor,
        totalMonth: this.cdbForm.value.mes,
      };

      this.http.post<CalculationResponse>('https://localhost:7078/api/investment/calculate-cdb', request).subscribe(
        (result) => {
          this.response = result;
          this.errorMessage = null;
        },
        (error) => {
          console.error(error);
          this.errorMessage = "An error occurred while calculating.";
        }
      );
    } else {
      this.errorMessage = "Please fill out the form correctly.";
    }
  }

  title = 'cdbcalc.client';
}
