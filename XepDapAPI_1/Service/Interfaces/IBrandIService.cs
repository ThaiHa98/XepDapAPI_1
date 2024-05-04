using Data.Dto;
using Data.Models;

namespace XepDapAPI_1.Service.Interfaces
{
    public interface IBrandIService
    {
        public Brand Create(Brand brand);
        string Update(BrandDto brandDto);
        bool Delete(int id);

    }
}
