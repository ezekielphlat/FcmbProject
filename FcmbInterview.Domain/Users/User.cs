using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FcmbInterview.Domain.Enum;
using FcmbInterview.Domain.Transactions;

namespace FcmbInterview.Domain.Users
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public UserTypes UserType { get; set; }
        public DateTime DateCreated { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
