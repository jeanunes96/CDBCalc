using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDBCalc.Domain.Model
{
    public class CalculationResponse
    {
        public decimal? InitialValue { get; set; }
        public int TotalMonths { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? Cdi { get; set; }
        public decimal? BankRate { get; set; }
    }
}
