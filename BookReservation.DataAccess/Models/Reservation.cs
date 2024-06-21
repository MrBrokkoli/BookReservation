using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BookReservation.DataAccess.Models
{
    public class Reservation : Entity
    {
        public int BookId { get; set; }

        public Book Book { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Comment { get; set; }
    }
}
