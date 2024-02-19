using Azure.Core;
using FcmbInterview.Application.Common.Interfaces.Persistence;
using FcmbInterview.Application.Common.Interfaces.Persistence.Common;
using FcmbInterview.Application.Common.Interfaces.Services;
using FcmbInterview.Domain.Enum;
using FcmbInterview.Domain.Transactions;
using FcmbInterview.Infrastructure.Data;
using FcmbInterview.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FcmbInterview.Infrastructure.Persistence
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IReferenceNumberGenerator _referenceNumberGenerator;
        private Dictionary<string, ITransferHandler> _handler = new Dictionary<string, ITransferHandler>();
        private const string BUSINESS_USER_TYPE = "BUSINESS";
        private const string RETAIL_USER_TYPE = "RETAIL";
        private const string FOURYEARS_USER_TYPE = "FOURYEARS";

        public TransactionRepository(ApplicationDbContext context, IReferenceNumberGenerator referenceNumberGenerator)
        {
            _context = context;
            _referenceNumberGenerator = referenceNumberGenerator;
            _handler.Add(BUSINESS_USER_TYPE, new HandleBusinessTransaction(context, referenceNumberGenerator));
            _handler.Add(RETAIL_USER_TYPE, new HandleRetailTransfer(context, referenceNumberGenerator));
            _handler.Add(FOURYEARS_USER_TYPE, new HandleFourYearsTransfer(context, referenceNumberGenerator));

        }

        public async Task<GenericResponse<string>> BuyAirtime(string networkProvider, string phoneNumber, string userName, decimal amount)
        {
            var user = _context.Users.Where(u => u.UserName == userName).FirstOrDefault();
            if (user == null)
            {
                //return error
                return new GenericResponse<string>()
                {
                    Data = string.Empty,
                    Message = "Cannot retrieve requesting user",
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    IsSuccess = false
                };
            }

            // save airtime transaction to db
            var newTrans = new Transaction();
            newTrans.NetworkProvider = networkProvider;
            newTrans.PhoneNumber = phoneNumber;
            newTrans.TransactionReferenceNumber = _referenceNumberGenerator.GetReferenceNumber(FOURYEARS_USER_TYPE);
            newTrans.TransactionType = TransactionTypes.AIRTIME;
            newTrans.Amount = amount;
            newTrans.Status = TransactionStatus.SUCCESSFULL;
            newTrans.UserId = user.Id;
            newTrans.DateOfTransaction = DateTime.UtcNow;       

            await _context.Transactions.AddAsync(newTrans);
            var result = await _context.SaveChangesAsync();

            if (result <= 0) throw new Exception("unable to create transaction");

            return new GenericResponse<string>()
            {
                Data = "Successful",
                Message = string.Empty,
                StatusCode = System.Net.HttpStatusCode.OK,
                IsSuccess = true
            };


        }

        public async Task<GenericResponse<string>> DoTransfer(string sendersAccount, string recieversAccount, string userName, decimal amount)
        {
            // get user by username
            var user = _context.Users.Where(u => u.UserName == userName).FirstOrDefault();
            if (user == null)
            {
                //return error
                return new GenericResponse<string>()
                {
                    Data = string.Empty,
                    Message = "Cannot retrieve requesting user",
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    IsSuccess = false
                };
            }

            try
            {
                // call appropriate handler
                var result = false;
                if (_handler.ContainsKey(user.UserType.ToString()))
                {

                    result = await _handler[user.UserType.ToString()].Handle(new TransferRequest(sendersAccount, recieversAccount, amount, user.Id));

                }
                else
                {
                    result = await _handler[FOURYEARS_USER_TYPE].Handle(new TransferRequest(sendersAccount, recieversAccount, amount, user.Id));

                }


                return new GenericResponse<string>()
                {
                    Data = "Successful",
                    Message = string.Empty,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    IsSuccess = true
                };

            }
            catch (Exception ex)
            {
                return new GenericResponse<string>()
                {
                    Data = ex.StackTrace ?? string.Empty,
                    Message = ex.Message,
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    IsSuccess = true
                };
            }
        }

        public async Task<GenericResponse<List<TransactionResponse>>> GetAllTransferTransaction()
        {

            var result = await _context.Transactions
                 .Where(r => r.TransactionType.ToString() == TransactionTypes.TRANSFER.ToString())
                .Select(t => new TransactionResponse(t.Id, t.SenderAccount, t.RecieverAccount, t.DiscountedAmount.ToString(), t.Amount.ToString(), t.Status.ToString(), t.DateOfTransaction.ToString(), t.TransactionReferenceNumber, t.User.UserName))
                .ToListAsync();

            return new GenericResponse<List<TransactionResponse>>()
            {
                Data = result,
                Message = string.Empty,
                StatusCode = System.Net.HttpStatusCode.OK,
                IsSuccess = true
            };
        }

        public async Task<GenericResponse<List<TransactionResponse>>> GetTransferTransactionsByAccountNumber(string accountNumber)
        {
            var result = await _context.Transactions.Where(d => d.TransactionType.ToString() == TransactionTypes.TRANSFER.ToString() &&(d.SenderAccount == accountNumber || d.RecieverAccount == accountNumber))
                .Select(t => new TransactionResponse(t.Id, t.SenderAccount, t.RecieverAccount, t.DiscountedAmount.ToString(), t.Amount.ToString(), t.Status.ToString(), t.DateOfTransaction.ToString(), t.TransactionReferenceNumber, t.User.UserName)).ToListAsync();

            return new GenericResponse<List<TransactionResponse>>()
            {
                Data = result,
                Message = string.Empty,
                StatusCode = System.Net.HttpStatusCode.OK,
                IsSuccess = true
            };
        }

    
        public async Task<GenericResponse<List<AirtimeTransactionResponse>>> GetAllAirtimeTransaction()
        {

            var result = await _context.Transactions
                .Where(r => r.TransactionType.ToString() == TransactionTypes.AIRTIME.ToString())
                .Select(t => new AirtimeTransactionResponse(t.Id, t.NetworkProvider, t.PhoneNumber,  t.Amount.ToString(), t.Status.ToString(), t.DateOfTransaction.ToString(), t.TransactionReferenceNumber, t.User.UserName))
                .ToListAsync();

            return new GenericResponse<List<AirtimeTransactionResponse>>()
            {
                Data = result,
                Message = string.Empty,
                StatusCode = System.Net.HttpStatusCode.OK,
                IsSuccess = true
            };
        }

        public async Task<GenericResponse<List<AirtimeTransactionResponse>>> GetAirtimeTransactionsByPhoneNumber(string phoneNumber)
        {
            var result = await _context.Transactions.Where(d => d.TransactionType.ToString() == TransactionTypes.AIRTIME.ToString() && d.PhoneNumber == phoneNumber)
               .Select(t => new AirtimeTransactionResponse(t.Id, t.NetworkProvider, t.PhoneNumber, t.Amount.ToString(), t.Status.ToString(), t.DateOfTransaction.ToString(), t.TransactionReferenceNumber, t.User.UserName)).ToListAsync();

            return new GenericResponse<List<AirtimeTransactionResponse>>()
            {
                Data = result,
                Message = string.Empty,
                StatusCode = System.Net.HttpStatusCode.OK,
                IsSuccess = true
            };
        }

     
    }
}
