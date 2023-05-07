﻿using ProniaBackEndProject.Data;
using ProniaBackEndProject.Model;

namespace ProniaBackEndProject.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
       
    }
}