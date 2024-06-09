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
    public class OrderItemController : ControllerBase
    {
        private IOrderService _orderService;
        private IMapper _mapper;

        public OrderItemController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetOrderItemById(int id)
        {
            OrderItem? orderItem = _orderService.GetOrderItemById(id);

            if(orderItem == null)
            {
                return NotFound(id);
            }

            return Ok(orderItem);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrderItem([FromBody] CreateOrderItemDto dto)
        {
            OrderItem orderItem = _mapper.Map<OrderItem>(dto);

            orderItem = _orderService.RegisterNewOrderItem(orderItem);

            if(orderItem == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(
                    nameof(GetOrderItemById),
                    new { id = orderItem.Id },
                    orderItem);
        }
    }
}
