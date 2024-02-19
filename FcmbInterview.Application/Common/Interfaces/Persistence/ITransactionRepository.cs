using FcmbInterview.Application.Common.Interfaces.Persistence.Common;
using FcmbInterview.Domain.Transactions;
using FcmbInterview.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FcmbInterview.Application.Common.Interfaces.Persistence
{
    public interface ITransactionRepository
    {
        Task<GenericResponse<List<TransactionResponse>>> GetAllTransferTransaction();
        Task<GenericResponse<List<AirtimeTransactionResponse>>> GetAllAirtimeTransaction();
        Task<GenericResponse<List<AirtimeTransactionResponse>>> GetAirtimeTransactionsByPhoneNumber(string phoneNumber);
        Task<GenericResponse<List<TransactionResponse>>> GetTransferTransactionsByAccountNumber(string accountNumber);

        Task<GenericResponse<string>> DoTransfer(string sendersAccount, string recieversAccount, string userName, decimal amount);

        Task<GenericResponse<string>> BuyAirtime(string networkProvider, string phoneNumber, string userName, decimal amount);



    }
    public record TransactionResponse(int transactionId, string senderAccount,string recieverAccount, string discountedAmount, string actualAmount, string status, string transactionDate, string referenceNumber, string userName );
    public record AirtimeTransactionResponse(int transactionId, string networkProvider,string phoneNumber, string actualAmount, string status, string transactionDate, string referenceNumber, string userName );
       

       
}
