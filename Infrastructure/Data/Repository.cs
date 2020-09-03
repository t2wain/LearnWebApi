using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Core.Specifications;
using Microsoft.EntityFrameworkCore.Query;
using AutoMapper;
using Core.Dtos;

namespace Infrastructure.Data
{
    public class Repository : IRepository
    {
        private readonly AdventureWorksContext _context;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Product> _prodRepo;

        public Repository(AdventureWorksContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
            this._prodRepo = new GenericRepository<Product>(this._context);
        }

        #region Customer

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await this._context.Customer.FindAsync(id);
        }

        public async Task<IReadOnlyList<Customer>> GetCustomersAsync()
        {
            return await this._context.Customer.ToListAsync();
        }

        #endregion

        #region Product

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var spec = new ProductSpec(id);
            return await this._prodRepo.GetById(spec);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecParams productParams)
        {
            var spec = new ProductSpec(productParams);
            return await this._prodRepo.GetListAsync(spec);
        }

        public async Task<int> GetProductsCountAsync(ProductSpecParams productParams)
        {
            var spec = new ProductSpec(productParams);
            return await this._prodRepo.GetCountAsync(spec);
        }

        public async Task<Pagination<ProductDto>> GetProductsDtoAsync(ProductSpecParams productParams)
        {
            var spec = new ProductSpec(productParams);
            return await this._prodRepo.GetPagination<ProductDto>(spec, this._mapper);
        }

        #endregion
    }
}
