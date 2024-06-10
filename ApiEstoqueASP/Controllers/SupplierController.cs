using ApiEstoqueASP.Data;
using ApiEstoqueASP.Data.DTOs;
using ApiEstoqueASP.Models;
using ApiEstoqueASP.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiEstoqueASP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        private IMapper _mapper;

        public SupplierController(ISupplierService supplierService, IMapper mapper)
        {
            _supplierService = supplierService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSupplier(int id)
        {
            Supplier? supplier = this._supplierService.GetSupplierById(id);

            if(supplier is null)
            {
                return NotFound(id);
            }

            ReadSupplierDto supplierDto = _mapper.Map<ReadSupplierDto>(supplier);

            return Ok(supplierDto);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult RegisterSupplier([FromBody] CreateSupplierDto dto)
        {
            Supplier supplier = _mapper.Map<Supplier>(dto);

            this._supplierService.CreateNewSupplier(supplier);

            ReadSupplierDto supplierDto = _mapper.Map<Supplier, ReadSupplierDto>(supplier);

            return CreatedAtAction
            (
                nameof(GetSupplier),
                new { id = supplier.Id },
                supplierDto
            );
        }


        
        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateSupplier(int id, [FromBody] UpdateSupplierDto dto)
        {
            Supplier? updatedSupplier = this._supplierService.UpdateSupplier(id, dto);

            if(updatedSupplier is null)
            {
                return NotFound(id);
            }

            return NoContent();
        }
        
    }
}
