using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using BookReservation.Models.Base;
using BookReservation.Services.Base.Interfaces;

namespace BookReservation.Controllers.Base
{
    public abstract class EntityController<TService, TCreateModel, TEditModel, TViewModel> : BaseServiceController<TService>
        where TService : IEntityService<TCreateModel, TEditModel, TViewModel>
        where TCreateModel : class, ICreateModel, new()
        where TEditModel : class, IEditModel, new()
        where TViewModel : class, IViewModel, new()
    {
        public EntityController(TService service) : base(service)
        {
        }

        /// <summary>
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TViewModel>> CreateAsync([FromBody] TCreateModel createModel)
        {
            return await Service.CreateAsync(createModel);
        }

        /// <summary>
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TViewModel>> GetAsync([BindRequired] int id)
        {
            return await Service.GetAsync(id);
        }

        /// <summary>
        /// </summary>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TViewModel>> UpdateAsync([FromBody] TEditModel editModel)
        {
            return await Service.UpdateAsync(editModel);
        }

        /// <summary>
        /// </summary>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteAsync([BindRequired] int id)
        {
            await Service.DeleteAsync(id);
            return Ok();
        }
    }
}
