using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using BookReservation.Controllers.Base;
using BookReservation.DataAccess.Models;
using BookReservation.DataAccess.Repositories.Base;
using BookReservation.Models;
using BookReservation.Models.Base;
using BookReservation.Models;
using BookReservation.Services;
using BookReservation.Models.Book;
using BookReservation.Exceptions;

namespace BookReservation.Controllers
{
    public class BookController : EntityController<IBookService, BookCreateModel, BookEditModel, BookViewModel>
    {
        public BookController(IBookService service) : base(service)
        {
        }

        /// <summary>
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ReserveAsync([FromBody] ReserveModel reserveModel)
        {
            try
            {
                await Service.ReserveAsync(reserveModel);
            }
            catch (BadRequestServiceException ex) 
            {
                return BadRequest(ex.Message);
            }
            
            return Ok();
        }

        /// <summary>
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CancelReserveAsync([FromBody] int bookId)
        {
            try
            {
                await Service.CancelReserveAsync(bookId);
            }
            catch (BadRequestServiceException ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok();
        }

        /// <summary>
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<ReservationBookViewModel>>> GetReservationBooksAsync()
        {
            return await Service.GetReservationBooksAsync();
        }

        /// <summary>
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<AvailableBookViewModel>>> GetAvailableBooksAsync()
        {
            return await Service.GetAvailableBooksAsync();
        }
    }
}
