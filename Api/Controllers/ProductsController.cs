using AutoMapper;
using Core.Dtos;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public ProductsController(IRepository repo, IMapper mapper)
        {
            this._repo = repo;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDto>>> Get([FromQuery] ProductSpecParams spec)
        {
            var pg = await this._repo.GetProductsDtoAsync(spec);
            return Ok(pg);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            var p = await this._repo.GetProductByIdAsync(id);
            if (p == null)
            {
                return NotFound();
            }
            var pDto = this._mapper.Map<ProductDto>(p);
            return Ok(pDto);
        }

    }
}
