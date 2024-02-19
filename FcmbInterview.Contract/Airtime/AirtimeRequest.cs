using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FcmbInterview.Contract.Airtime
{
    public record AirtimeRequest(string networkProvider, decimal amount, string phoneNumber);
   
}
