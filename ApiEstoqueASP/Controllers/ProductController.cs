﻿using ApiEstoqueASP.Data;
using ApiEstoqueASP.Data.DTOs;
using ApiEstoqueASP.Models;
using ApiEstoqueASP.Services;
using ApiEstoqueASP.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiEstoqueASP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private ApiEstoqueDbContext _context;
        private IProductService _productService;
        private IMapper _mapper;

        public ProductController(ApiEstoqueDbContext context, IProductService productService, IMapper mapper)
        {
            _context = context;
            _productService = productService;
            _mapper = mapper;
        }


        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductById(int id)
        {
            Product? product = _productService.GetProductById(id);

            if(product == null)
            {
                return NotFound(id);
            }

            ReadProductDto productDto = _mapper.Map<ReadProductDto>(product);

            return Ok(productDto);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)] // Para documentação de API. Informa qual o status code de resposta esperado
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto)
        {
            if(dto.Price <= 0)
            {
                return BadRequest();
            }

            Product product = _mapper.Map<Product>(dto);

            Product newProduct = _productService.CreateNewProduct(product);

            ReadProductDto productDto = _mapper.Map<ReadProductDto>(product);

            // "CreatedAtAction" => Retorna o objeto criado(ou registrado) com um header para acessar o mesmo com status code 201
            return CreatedAtAction
            (
                nameof(GetProductById),
                new { id = product.Id },
                productDto
            );
        }


        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDto dto)
        {
            if(dto.Price <= 0)
            {
                return BadRequest();
            }

            Product? product = this._productService.UpdateProduct(id, dto);

            if(product is null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
