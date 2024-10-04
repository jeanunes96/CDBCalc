using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDBCalc.Domain.Model
{
    public class CalculationRequest
    {
        public decimal InitialValue { get; set; }
        public int TotalMonth { get; set; }
    }
}
