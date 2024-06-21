using BookReservation.Models.Base;

namespace BookReservation.Models.Book
{
    public class BookEditModel : IEditModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string? Author { get; set; }
    }
}
