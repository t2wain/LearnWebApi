using Infrastructure.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Core.Dtos;
using System.Collections.Generic;

namespace ApiTest
{
    public class MapperTest : IClassFixture<DbFixture>
    {
        private readonly DbFixture _df;

        public MapperTest(DbFixture df)
        {
            this._df = df;
        }

        [Fact]
        public async void Should_Map_To_Product_DTO()
        {
            var lst = await this._df.Repo.GetProductsAsync();
            var lstDto = this._df.Mapper.Map<IEnumerable<ProductDto>>(lst);
            Assert.NotEmpty(lstDto);
        }

        [Fact]
        public async void Should_Map_To_Customer_DTO()
        {
            var lst = await this._df.Repo.GetCustomersAsync();
            var lstDto = this._df.Mapper.Map<IEnumerable<CustomerDto>>(lst);
            Assert.NotEmpty(lstDto);
        }
    }
}
