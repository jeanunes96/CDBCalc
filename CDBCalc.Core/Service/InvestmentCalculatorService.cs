using CDBCalc.Core.Service.Interface;
using CDBCalc.Domain.Model;

namespace CDBCalc.Core.Service
{
    public class InvestmentCalculatorService : IInvestmentCalculatorService
    {
        private const decimal CdiRate = 0.009m;
        private const decimal BankRate = 1.08m;

        public CalculationResponse CalculateCdb(CalculationRequest request)
        {
            decimal finalAmount = CalculateFinalAmount(request.InitialValue, request.TotalMonth, CdiRate, BankRate);
            decimal tax = CalculateTax(finalAmount - request.InitialValue, request.TotalMonth);
            decimal netAmount = finalAmount - tax;

            var result = new CalculationResponse
            {
                GrossAmount = finalAmount,
                NetAmount = netAmount,
                TaxAmount = tax,
                InitialValue = request.InitialValue,
                Cdi = CdiRate,
                BankRate = BankRate,
                TotalMonths = request.TotalMonth
            };

            return result;
        }

        private static decimal CalculateFinalAmount(decimal initialValue, int months, decimal cdi, decimal bankRate)
        {
            return CalculateFinalAmountOverMonths(initialValue, months, cdi, bankRate);
        }

        private static decimal CalculateFinalAmountOverMonths(decimal initialValue, int months, decimal cdi, decimal bankRate)
        {
            decimal amount = initialValue;

            for (int i = 0; i < months; i++)
            {
                amount *= 1 + (cdi * bankRate);
            }
            return amount;
        }

        private static decimal CalculateTax(decimal earnings, int months)
        {
            decimal rate = GetTaxRate(months);
            return earnings * rate;
        }

        private static decimal GetTaxRate(int months)
        {
            if (months <= 6) return 0.225m;
            if (months <= 12) return 0.20m;
            if (months <= 24) return 0.175m;
            return 0.15m;
        }
    }
}
