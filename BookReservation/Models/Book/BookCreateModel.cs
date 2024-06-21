using BookReservation.Models.Base;

namespace BookReservation.Models.Book
{
    public class BookCreateModel : ICreateModel
    {
        public string Title { get; set; }

        public string? Author { get; set; }
    }
}
