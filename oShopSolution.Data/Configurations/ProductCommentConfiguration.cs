using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using oShopSolution.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.Data.Configurations
{
    public class ProductCommentConfiguration : IEntityTypeConfiguration<ProductComment>
    {
        public void Configure(EntityTypeBuilder<ProductComment> builder)
        {
            builder.ToTable("Comment");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();


            builder.HasOne(x => x.Product).WithMany(x => x.ProductComments).HasForeignKey(x => x.ProductId);

        }
    }
}
