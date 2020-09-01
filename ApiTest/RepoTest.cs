using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ApiTest
{
    public class RepoTest : IClassFixture<DbFixture>
    {
        private readonly DbFixture _df;

        public RepoTest(DbFixture df)
        {
            this._df = df;
        }

        [Fact]
        public async void Should_Return_Customers()
        {
            var lst = await this._df.Repo.GetCustomersAsync();
            Assert.NotEmpty(lst);
        }

        [Fact]
        public async void Should_Return_Products()
        {
            var lst = await this._df.Repo.GetProductsAsync();
            Assert.NotEmpty(lst);
        }
    }
}
