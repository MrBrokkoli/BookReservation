using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BookReservation.DataAccess.Models
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
