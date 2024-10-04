import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms'; // Importando ReactiveFormsModule
import { AppComponent } from './app.component';

describe('AppComponent', () => {
  let component: AppComponent;
  let fixture: ComponentFixture<AppComponent>;
  let httpMock: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AppComponent],
      imports: [HttpClientTestingModule, ReactiveFormsModule] // Importando ReactiveFormsModule
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should create the app', () => {
    expect(component).toBeTruthy();
  });

  it('should calculate CDB and retrieve response from the server', () => {
    const mockResponse = {
      initialValue: 1000,
      totalMonths: 12,
      grossAmount: 1200,
      taxAmount: 200,
      netAmount: 1000,
      cdi: 5.5,
      bankRate: 2.5
    };

    // Configurando o formulário simulado
    component.cdbForm.setValue({ valor: 1000, mes: 12 });
    component.calculateCdb();

    const req = httpMock.expectOne('https://localhost:7078/api/investment/calculate-cdb');
    expect(req.request.method).toEqual('POST');
    expect(req.request.body).toEqual({ initialValue: 1000, totalMonth: 12 });
    
    // Simulando a resposta do servidor
    req.flush(mockResponse);

    // Verificando se a resposta foi processada corretamente
    expect(component.response).toEqual(mockResponse);
    expect(component.errorMessage).toBeNull(); // Verifica se não há mensagem de erro
  });

  it('should handle error response from the server', () => {
    component.cdbForm.setValue({ valor: 1000, mes: 12 });

    // Chamada ao método que simula a requisição
    component.calculateCdb();

    const req = httpMock.expectOne('https://localhost:7078/api/investment/calculate-cdb');
    req.flush('Something went wrong', { status: 500, statusText: 'Server Error' });

    // Verifica se a mensagem de erro foi definida
    expect(component.errorMessage).toEqual('An error occurred while calculating.');
  });
});
