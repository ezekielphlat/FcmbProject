using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FcmbInterview.Contract.Transaction
{
    public record DiscountRequest(string name, string userType, string percentage, double limitedAmount);
   
}
