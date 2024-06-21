using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookReservation.DataAccess.Models;

namespace BookReservation.DataAccess.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Book");
            builder.HasKey(e => e.Id);
            builder
                .Property(e => e.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Property(e => e.Title)
                .IsRequired();
        }
    }
}
