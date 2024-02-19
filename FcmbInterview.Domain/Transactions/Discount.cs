using FcmbInterview.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FcmbInterview.Domain.Transactions
{
    public class Discount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UserTypes UserType { get; set; }
        public double Percentage { get; set; }
        public double DiscountLimit { get; set; }


    }
}
