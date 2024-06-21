using Microsoft.AspNetCore.Mvc;

namespace BookReservation.Controllers.Base
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BaseController : ControllerBase
    {
        public BaseController()
        {
        }
    }
}
