﻿using Data.Dto;
using Data.Models;

namespace XepDapAPI_1.Service.Interfaces
{
    public interface IProduct_DetailIService
    {
        Task<Product_Details> Create(Product_DetailDto product_DetailDto);
        Task<UpdateProduct_DetailsDto> Update(int Id, UpdateProduct_DetailsDto updateProduct_DetailsDto);
        bool Delete(int Id);
    }
}
