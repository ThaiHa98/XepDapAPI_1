using Data.Dto;

namespace XeDapAPI.Service.Interfaces
{
    public interface ISlideIService
    {
        string Create(SlideDto slideDto, IFormFile image);
        string Update(UpdateSlideDto updateSlideDto);
        bool Delete(int Id);
    }
}
