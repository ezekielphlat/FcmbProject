using FcmbInterview.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FcmbInterview.Domain.Transactions;
using FcmbInterview.Domain.Enum;

namespace FcmbInterview.Infrastructure.Data.Configurations
{
    public class TransactionEntityConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(o => o.Amount).HasPrecision(16,2).IsRequired();         
            builder.Property(o => o.DiscountedAmount).HasPrecision(16, 2);
            builder.HasIndex(o => o.TransactionReferenceNumber).IsUnique();
            builder.Property(s => s.TransactionType).HasConversion(
                o => o.ToString(),
                o => (TransactionTypes)Enum.Parse(typeof(TransactionTypes), o));


        }
    }
}
