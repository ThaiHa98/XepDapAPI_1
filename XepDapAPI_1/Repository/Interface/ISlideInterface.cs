using Data.Models;

namespace XeDapAPI.Repository.Interface
{
    public interface ISlideInterface
    {
        ICollection<Slide> GetSlides();
    }
}
