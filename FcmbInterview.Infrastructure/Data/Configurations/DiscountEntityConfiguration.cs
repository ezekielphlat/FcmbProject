using FcmbInterview.Domain.Enum;
using FcmbInterview.Domain.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FcmbInterview.Infrastructure.Data.Configurations
{
    public class DiscountEntityConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Percentage).IsRequired();
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.DiscountLimit).IsRequired();
            builder.Property(s => s.UserType).HasConversion(
               o => o.ToString(),
               o => (UserTypes)Enum.Parse(typeof(UserTypes), o));
        }
    }
}
