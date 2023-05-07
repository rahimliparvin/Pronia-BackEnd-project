﻿using Microsoft.EntityFrameworkCore;
using ProniaBackEndProject.Data;
using ProniaBackEndProject.Model;
using ProniaBackEndProject.Services.Interfaces;

namespace ProniaBackEndProject.Services
{
    public class BrandService : IBrandService
    {
        private readonly AppDbContext _context;
        public BrandService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Brand>> GetAllAsync() => await _context.Brands.ToListAsync();


        public async Task<Brand> GetByIdAsync(int id) => await _context.Brands.FindAsync(id);
       
    }
}