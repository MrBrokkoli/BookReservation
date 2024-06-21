namespace BookReservation.Exceptions
{
    public class BadRequestServiceException : ServiceException
    {
        public BadRequestServiceException()
        {
        }

        public BadRequestServiceException(string message)
            : base(message)
        {
        }
    }
}
