using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FcmbInterview.Domain.Enum;
using FcmbInterview.Domain.Users;

namespace FcmbInterview.Domain.Transactions
{
    public class Transaction
    {
        public int Id { get; set; }
        public TransactionTypes TransactionType { get; set; }
        public string? SenderAccount { get; set; }
        public string? NetworkProvider { get; set; }
        public string? PhoneNumber { get; set; }
        public string? RecieverAccount { get; set; }
        public decimal? DiscountedAmount { get; set; }
        public decimal Amount { get; set; }
        public TransactionStatus Status { get; set; }
        public string TransactionReferenceNumber { get; set; }
        public int DiscountId { get; set; }
        public Discount Discount { get; set; }
        public int UserId { get; set; } // key to user
        public User User { get; set; }
        public DateTime DateOfTransaction { get; set; }

    }
}
