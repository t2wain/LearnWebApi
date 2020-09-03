using Core.Specifications;
using System.Linq;
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
        public async void Should_Return_Products_Count50_NameAsc()
        {
            var sp = new ProductSpecParams() { Sort = "nameAsc", PageSize = 50 };
            var s = new ProductSpec(sp);
            var lst = await this._df.ProductRepo.GetListAsync(s);
            Assert.True(lst.Count == sp.PageSize);
            Assert.StartsWith("A", lst.First().Name);
        }

        [Fact]
        public async void Should_Return_Products_Page2_Count50_NameAsc()
        {
            var sp = new ProductSpecParams() { Sort = "nameAsc", PageIndex=2, PageSize = 50 };
            var s = new ProductSpec(sp);
            var lst = await this._df.ProductRepo.GetListAsync(s);
            Assert.True(lst.Count == sp.PageSize);
            Assert.False(lst.First().Name.StartsWith("A"));
            Assert.False(lst.First().Name.StartsWith("W"));
        }

        [Fact]
        public async void Should_Return_Products_Count50_NameDesc()
        {
            var sp = new ProductSpecParams() { Sort = "nameDesc", PageSize = 50 };
            var s = new ProductSpec(sp);
            var lst = await this._df.ProductRepo.GetListAsync(s);
            Assert.True(lst.Count == sp.PageSize);
            Assert.StartsWith("W", lst.First().Name);
        }

        [Fact]
        public async void Should_Return_A_Product()
        {
            var s = new ProductSpec(680);
            var p = await this._df.ProductRepo.GetById(s);
            Assert.NotNull(p);
        }

        [Fact]
        public async void Should_Not_Return_A_Product()
        {
            var s = new ProductSpec(1);
            var p = await this._df.ProductRepo.GetById(s);
            Assert.Null(p);
        }

        [Fact]
        public async void Should_Return_ProductDtos_Filter_Category()
        {
            var sp = new ProductSpecParams() { Sort = "nameAsc", ProductCategoryId = 25 };
            var pg = await this._df.Repo.GetProductsDtoAsync(sp);
            Assert.True(pg.PageSize == sp.PageSize);
            Assert.True(pg.PageIndex == sp.PageIndex);
            Assert.True(pg.Count < 295);
            Assert.False(pg.Data.First().Name.StartsWith("A"));
            Assert.False(pg.Data.First().Name.StartsWith("W"));
        }

    }
}
