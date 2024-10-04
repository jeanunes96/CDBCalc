using CDBCalc.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CDBCalc.Core.Service.Interface
{
    public interface IInvestmentCalculatorService
    {
        CalculationResponse CalculateCdb(CalculationRequest request);
    }
}
