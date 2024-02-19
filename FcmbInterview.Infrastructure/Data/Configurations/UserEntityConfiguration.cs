using FcmbInterview.Domain.Enum;
using FcmbInterview.Domain.Transactions;
using FcmbInterview.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FcmbInterview.Infrastructure.Data.Configurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(s => s.UserType).HasConversion(
                o => o.ToString(),
                o => (UserTypes)Enum.Parse(typeof(UserTypes), o));
            //builder.HasMany<Transaction>().WithOne(x=> x.User).HasForeignKey(f => f.UserId).OnDelete(DeleteBehavior.Cascade);
            //builder.HasMany<Transaction>().WithOne(x=> x.Reciever).HasForeignKey(f => f.RecieverId).OnDelete(DeleteBehavior.Cascade); 

        }
    }
}
