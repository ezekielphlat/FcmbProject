using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FcmbInterview.Contract.Transfer
{
    public record  TransferRequest(string sourceAccount, string destinationAccount, decimal amount);

}
