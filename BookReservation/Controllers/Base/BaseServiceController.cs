using BookReservation.Services.Base.Interfaces;

namespace BookReservation.Controllers.Base
{
    public abstract class BaseServiceController<TService> : BaseController where TService : IService
    {
        protected TService Service { get; }

        public BaseServiceController(TService service)
        {
            Service = service;
        }
    }
}
