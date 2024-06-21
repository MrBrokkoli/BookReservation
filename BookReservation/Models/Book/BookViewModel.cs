using BookReservation.Models.Base;

namespace BookReservation.Models.Book
{
    public class BookViewModel : IViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string? Author { get; set; }

        public bool IsDeleted { get; set; }
    }
}
