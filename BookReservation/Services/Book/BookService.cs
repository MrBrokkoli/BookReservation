using Microsoft.EntityFrameworkCore;
using System.Net;
using BookReservation.DataAccess.Models;
using BookReservation.DataAccess.Repositories.Base;
using BookReservation.Exceptions;
using BookReservation.Models;
using BookReservation.Models.Book;
using BookReservation.Services.Base.Interfaces;
using BookReservation.Models.Base;

namespace BookReservation.Services
{
    public class BookService : IEntityService<BookCreateModel, BookEditModel, BookViewModel>, IBookService
    {
        private IRepository<Book> _bookRepository;

        private IRepository<Reservation> _reservationRepository;

        public BookService(IRepository<Book> bookRepository, IRepository<Reservation> reservationRepository)
        {
            _bookRepository = bookRepository;
            _reservationRepository = reservationRepository;
        }

        public async Task<BookViewModel> CreateAsync(BookCreateModel createModel)
        {
            // можно добавить Mapper
            var model = new Book()
            {
                Author = createModel.Author,
                Title = createModel.Title,
            };

            var savedModel = await _bookRepository.SaveAsync(model);

            return new BookViewModel()
            {
                Id = savedModel.Id,
                Author = savedModel.Author,
                Title = savedModel.Title,
                IsDeleted = savedModel.IsDeleted,
            };
        }

        public async Task DeleteAsync(int id)
        {
            var model = await _bookRepository.GetAsync(id);
            if (model == null)
                throw new BadRequestServiceException($"Not found book Id={id}");
            model.IsDeleted = true;
            await _bookRepository.UpdateAsync(model);
        }

        public async Task<BookViewModel> GetAsync(int id)
        {
            var model = await _bookRepository.GetAsync(id);
            if (model == null)
                throw new BadRequestServiceException($"Not found book Id={id}");

            return new BookViewModel()
            {
                Id = model.Id,
                Author = model.Author,
                Title = model.Title,
                IsDeleted = model.IsDeleted,
            };
        }

        public async Task<BookViewModel> UpdateAsync(BookEditModel editModel)
        {
            var model = await _bookRepository.GetAsync(editModel.Id);
            if (model == null)
                throw new BadRequestServiceException($"Not found book Id={editModel.Id}");

            model.Author = editModel.Author;
            model.Title = editModel.Title;

            await _bookRepository.UpdateAsync(model);

            return new BookViewModel()
            {
                Id = model.Id,
                Author = model.Author,
                Title = model.Title,
                IsDeleted = model.IsDeleted,
            };
        }

        public async Task ReserveAsync(ReserveModel reserveModel)
        {
            var book = await _bookRepository.GetAsync(reserveModel.BookId);
            if (book == null)
                throw new BadRequestServiceException($"Not found book Id={reserveModel.BookId}");
            if (book.IsDeleted)
                throw new BadRequestServiceException($"Book was delete");
            var isExists = await _reservationRepository.Query.AnyAsync(x => x.BookId == reserveModel.BookId && x.StartDate.HasValue && !x.EndDate.HasValue);

            if (isExists)
                throw new BadRequestServiceException($"The book on reserve");

            var model = new Reservation();
            model.BookId = reserveModel.BookId;
            model.Comment = reserveModel.Comment;
            model.StartDate = DateTime.UtcNow;

            await _reservationRepository.SaveAsync(model);
        }

        public async Task CancelReserveAsync(int bookId)
        {
            var book = await _bookRepository.GetAsync(bookId);
            if (book == null)
                throw new BadRequestServiceException($"Not found book Id={bookId}");
            var model = await _reservationRepository.Query.FirstOrDefaultAsync(x => x.BookId == bookId && x.StartDate.HasValue && !x.EndDate.HasValue);

            if (model == null)
                throw new BadRequestServiceException($"The book is not in reserve");

            model.EndDate = DateTime.UtcNow;
            
            await _reservationRepository.UpdateAsync(model);
        }

        public async Task<List<ReservationBookViewModel>> GetReservationBooksAsync()
        {
            var reservations = await _reservationRepository.UntrackedQuery.Include(x => x.Book).Where(x => x.StartDate.HasValue && !x.EndDate.HasValue).ToListAsync();

            return reservations.Select(reservation => new ReservationBookViewModel
            {
                Id = reservation.Book?.Id ?? -1,
                Title = reservation.Book?.Title ?? "",
                Author = reservation.Book?.Author,
                Comment = reservation.Comment,
            }).ToList();
        }

        public async Task<List<AvailableBookViewModel>> GetAvailableBooksAsync()
        {
            var books = await _bookRepository.UntrackedQuery.Include(x => x.Reservations).Where(x => x.Reservations.Count(r => r.StartDate.HasValue && !r.EndDate.HasValue) == 0 && x.IsDeleted == false).ToListAsync();

            return books.Select(book => new AvailableBookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
            }).ToList();
        }
    }
}
