using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crayon.ExchangeRateHost.Core.Models
{
    public class ExchangeRate
    {
        public string Currency { get; set; }
        public decimal Rate { get; set; }
    }
}
