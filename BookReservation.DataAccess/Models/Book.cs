using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReservation.DataAccess.Models
{
    public class Book : Entity
    {
        public string Title { get; set; }

        public string? Author { get; set; }

        public bool IsDeleted { get; set; }

        public IList<Reservation> Reservations { get; set; }
    }
}
