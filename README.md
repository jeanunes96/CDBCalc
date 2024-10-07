# Projeto desenvolvido para desafio técnico B3

- Projeto Web: Desenvolvido com Angular CLI.
- Web API: Desenvolvida em .NET 8.0.


## Instruções para Execução

	Setar como projeto de inicialização o CDBCalc.Server e executar, primeiro irá carregar o swagger com o endpoint e depois será gerado o build
	do front. Estará disponível em: localhost:4200

## Testes da Web API

InvestmentControllerTests:

-CalculateCdb_ValidRequest_ReturnsOkResult() ->  Tem como objetivo testar uma solicitação válida para o método e se o retorno é um resultado HTTP200;

-CalculateCdb_NullRequest_ReturnsBadRequest() -> Tem como objetivo testar uma solicitação nula pra o método e se o retorno é um resultado HTTP400;


InvestmentCalculatorServiceTests:

-CalculateCdb_ValidRequest_ReturnsCorrectResponse() -> Tem como objetivo testar uma solicitação válida e se o retorno está correto;

-CalculateCdb_ZeroInitialValue_ReturnsCorrectResponse() -> Tem como objetivo testar uma solicitação com valor inicial zero e se o retorno está correto;
