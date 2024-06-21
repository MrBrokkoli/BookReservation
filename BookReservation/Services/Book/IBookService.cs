using BookReservation.Models;
using BookReservation.Models.Base;
using BookReservation.Models.Book;
using BookReservation.Services.Base.Interfaces;

namespace BookReservation.Services
{
    public interface IBookService : IEntityService<BookCreateModel, BookEditModel, BookViewModel>
    {
        Task ReserveAsync(ReserveModel reserveModel);

        Task CancelReserveAsync(int bookId);

        Task<List<ReservationBookViewModel>> GetReservationBooksAsync();

        Task<List<AvailableBookViewModel>> GetAvailableBooksAsync();
    }
}
