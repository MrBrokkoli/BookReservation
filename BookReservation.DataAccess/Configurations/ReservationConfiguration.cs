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
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservation");
            builder.HasKey(e => e.Id);
            builder
                .Property(e => e.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.HasIndex(e => new { e.BookId, e.StartDate, e.EndDate });
        }
    }
}
