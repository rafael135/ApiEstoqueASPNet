using ApiEstoqueASP.Data;
using ApiEstoqueASP.Data.DTOs;
using ApiEstoqueASP.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiEstoqueASP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SupplierController : ControllerBase
    {
        private ApiEstoqueDbContext _context;
        private IMapper _mapper;

        public SupplierController(ApiEstoqueDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetSupplier(int id)
        {
            Supplier? supplier = _context.Suppliers.FirstOrDefault(sup => sup.Id == id);

            if(supplier == null)
            {
                return NotFound(id);
            }

            ReadSupplierDto supplierDto = _mapper.Map<ReadSupplierDto>(supplier);

            return Ok(supplierDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult RegisterSupplier([FromBody] CreateSupplierDto dto)
        {
            Supplier supplier = _mapper.Map<Supplier>(dto);

            this._context.Suppliers.Add(supplier);
            this._context.SaveChanges();

            Console.WriteLine(supplier.RegisterDate);

            ReadSupplierDto supplierDto = _mapper.Map<Supplier, ReadSupplierDto>(supplier);

            

            return CreatedAtAction
            (
                nameof(GetSupplier),
                new { id = supplier.Id },
                supplierDto
            );
        }


        /*
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult UpdateSupplier(int id, [FromBody] UpdateSupplierDto)
        {

        }
        */
    }
}
