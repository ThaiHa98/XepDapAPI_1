﻿using Data.Dto;
using Data.Models;

namespace XepDapAPI_1.Repository.Interface
{
    public interface IProductsInterface
    {
        List<ProductGetAllInfDto> GetAllProducts();
        List<ProductBrandInfDto> GetAllBrandName(string keyword);
        List<ProductTypeInfDto> GetAllTypeName(string keyword,int limit = 6);
        List<ProductPriceHasDecreasedInfDto> SearchProductsByPriceHasDecreased();
        Products GetProductsId(int Id);
        List<ProductGetAllInfPriceDto> GetProductsWithinPriceRangeAndBrand();
    }
}
