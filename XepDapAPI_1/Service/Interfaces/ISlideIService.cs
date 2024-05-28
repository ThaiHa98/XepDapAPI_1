using Data.Dto;

namespace XeDapAPI.Service.Interfaces
{
    public interface ISlideIService
    {
        string Create(SlideDto slideDto, IFormFile image);
        string Update(UpdateSlideDto updateSlideDto);
        bool Delete(int Id);
        byte[] GetSileBytesImage(string imagePath);
        byte[] GetSlideBytesImageid4(string imagePath);
        byte[] GetSlideBytesImageid5(string imagePath);
    }
}
