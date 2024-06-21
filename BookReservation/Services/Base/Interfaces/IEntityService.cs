using BookReservation.Models.Base;

namespace BookReservation.Services.Base.Interfaces
{
    public interface IEntityService<TCreateModel, TEditModel, TViewModel> : IService
        where TCreateModel : class, ICreateModel, new()
        where TEditModel : class, IEditModel, new()
        where TViewModel : class, IViewModel, new()
    {
        Task<TViewModel> CreateAsync(TCreateModel createModel);

        Task<TViewModel> GetAsync(int id);

        Task<TViewModel> UpdateAsync(TEditModel editModel);

        Task DeleteAsync(int id);
    }
}
