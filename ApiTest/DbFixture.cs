using AutoMapper;
using Core.Dtos;
using Core.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTest
{
    public class DbFixture : IDisposable
    {
        public DbFixture()
        {
            this.DbContext = new AdventureWorksContext();
            this.Repo = new Repository(this.DbContext);
            this.Mapper = MapperProfile.GetMapper();
        }

        public AdventureWorksContext DbContext { get; set; }
        public IRepository Repo { get; set; }
        public IMapper Mapper { get; set; }

        public void Dispose()
        {
            if (this.DbContext != null)
            {
                this.DbContext.Dispose();
                this.DbContext = null;
            }
        }
    }
}
