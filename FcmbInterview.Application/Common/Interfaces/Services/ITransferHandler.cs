using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FcmbInterview.Application.Common.Interfaces.Services
{
    public interface ITransferHandler
    {
        Task<bool> Handle(TransferRequest request);
    }

   public record TransferRequest(string sourceAccount, string destinationAccount, decimal amount, int userId);
   

}
