using Data.Dto;
using Type = Data.Models.Type;
namespace XeDapAPI.Service.Interfaces
{
    public interface ITypeIService
    {
        public Type Create(Type type);
        string Update(TypeDto typeDto);
        bool Delete(int typeId);
    }
}
