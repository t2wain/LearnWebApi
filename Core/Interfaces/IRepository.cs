using Core.Dtos;
using Core.Entities;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRepository
    {
        Task<IReadOnlyList<Customer>> GetCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecParams productParams);
        Task<Product> GetProductByIdAsync(int id);
        Task<Pagination<ProductDto>> GetProductsDtoAsync(ProductSpecParams productParams);
    }
}
