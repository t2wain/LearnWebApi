using Core.Entities;
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
        Task<IReadOnlyList<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
    }
}
