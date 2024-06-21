namespace BookReservation.Exceptions
{
    public abstract class ServiceException : Exception
    {
        public ServiceException()
            : base()
        {
        }

        public ServiceException(string message)
            : base(message)
        {
        }
    }
}
