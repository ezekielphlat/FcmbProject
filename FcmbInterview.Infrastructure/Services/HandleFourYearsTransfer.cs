using FcmbInterview.Application.Common.Interfaces.Services;
using FcmbInterview.Domain.Enum;
using FcmbInterview.Domain.Transactions;
using FcmbInterview.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FcmbInterview.Infrastructure.Services
{
    public class HandleFourYearsTransfer : ITransferHandler
    {

        private const string FOURYEARS_USER_TYPE = "FOURYEARS";

        private readonly ApplicationDbContext _context;
        private readonly IReferenceNumberGenerator _referenceNumberGenerator;


        public HandleFourYearsTransfer(ApplicationDbContext context, IReferenceNumberGenerator referenceNumberGenerator)
        {
            this._context = context;
            this._referenceNumberGenerator = referenceNumberGenerator;
        }
        public async Task<bool> Handle(TransferRequest request)
        {
            // get discount configuration
            var discountConfig = _context.Discount.Where(d => d.UserType.ToString() == FOURYEARS_USER_TYPE).SingleOrDefault();
            if (discountConfig == null)
            {

                return false;

            }
            var newTrans = new Transaction();
            newTrans.SenderAccount = request.sourceAccount;
            newTrans.RecieverAccount = request.destinationAccount;
            newTrans.SenderAccount = request.sourceAccount;
            newTrans.TransactionReferenceNumber = _referenceNumberGenerator.GetReferenceNumber(FOURYEARS_USER_TYPE);
            newTrans.TransactionType = TransactionTypes.TRANSFER;
            newTrans.Amount = request.amount;
            newTrans.Status = TransactionStatus.SUCCESSFULL;
            newTrans.UserId = request.userId;
            newTrans.DateOfTransaction = DateTime.UtcNow;


            if (IsDiscountApplied(request.userId))
            {
                // use discount percentage rate to calculate discounted amount
                double discountRate = discountConfig.Percentage;
                double discountedAmount = (double)request.amount * (discountRate / 100.0);
                newTrans.DiscountedAmount = (decimal)discountedAmount;
                newTrans.DiscountId = discountConfig.Id;
            }

            await _context.Transactions.AddAsync(newTrans);
            var result = await _context.SaveChangesAsync();
            if (result <= 0)
            {
                return false;

            }
            return true;
        }

        public bool IsDiscountApplied(int userId)
        {
            DateTime currentDate = DateTime.UtcNow;
            // get all user transactions
            var usersTransacitons = _context.Transactions.Where(t => t.UserId == userId && (t.DateOfTransaction.Month == currentDate.Month && t.DateOfTransaction.Year == currentDate.Year)).Count();
            // check if transaction count is more then 3

            if (usersTransacitons >= 3)
            {
                return false;
            }
            return true;
        }
    }

    
}
