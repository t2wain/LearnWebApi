using Infrastructure.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ApiTest
{
    public class DbTest : IClassFixture<DbFixture>
    {
        private readonly DbFixture _df;

        public DbTest(DbFixture df)
        {
            this._df = df;
        }

        [Fact]
        public async void Should_Return_Addresses()
        {
            var lst = await this._df.DbContext.Address.ToListAsync();
            Assert.NotEmpty(lst);
        }

        [Fact]
        public async void Should_Return_Customers()
        {
            var lst = await this._df.DbContext.Customer.ToListAsync();
            Assert.NotEmpty(lst);
        }

        [Fact]
        public async void Should_Return_Addresses_Of_Customers()
        {
            var lst = await this._df.DbContext.Customer
                .Include(c => c.CustomerAddress)
                .ThenInclude(ca => ca.Address)
                .SelectMany(c => c.CustomerAddress)
                .Select(ca => ca.Address)
                .ToListAsync();
            Assert.NotEmpty(lst);
        }

        [Fact]
        public async void Should_Return_CustomerAddressess()
        {
            var lst = await this._df.DbContext.CustomerAddress.ToListAsync();
            Assert.NotEmpty(lst);
        }

        [Fact]
        public async void Should_Return_Products()
        {
            var lst = await this._df.DbContext.Product
                .Include(p => p.ProductCategory)
                .Include(p => p.ProductModel)
                .ToListAsync();
            Assert.NotEmpty(lst);
        }

        [Fact]
        public async void Should_Return_ProductCategories()
        {
            var lst = await this._df.DbContext.ProductCategory.ToListAsync();
            Assert.NotEmpty(lst);
        }

        [Fact]
        public async void Should_Return_ProductDescriptions()
        {
            var lst = await this._df.DbContext.ProductDescription.ToListAsync();
            Assert.NotEmpty(lst);
        }

        [Fact]
        public async void Should_Return_ProductModels()
        {
            var lst = await this._df.DbContext.ProductModel.ToListAsync();
            Assert.NotEmpty(lst);
        }

        [Fact]
        public async void Should_Return_ProductModelProductDescriptions()
        {
            var lst = await this._df.DbContext.ProductModelProductDescription.ToListAsync();
            Assert.NotEmpty(lst);
        }

        [Fact]
        public async void Should_Return_SalesOrderDetails()
        {
            var lst = await this._df.DbContext.SalesOrderDetail
                .Include(d => d.Product)
                .ToListAsync();
            Assert.NotEmpty(lst);
        }

        [Fact]
        public async void Should_Return_SalesOrderHeader()
        {
            var lst = await this._df.DbContext.SalesOrderHeader
                .Include(o => o.Customer)
                .Include(o => o.BillToAddress)
                .Include(o => o.ShipToAddress)
                .ToListAsync();
            Assert.NotEmpty(lst);
        }
    }
}
