using Data.Dto;
using Data.Models;

namespace XepDapAPI_1.Repository.Interface
{
    public interface IProducts_DrtailInterface
    {
        public ProductDetailGetInfDto Getproducts_Detail(int productId);
    }
}
