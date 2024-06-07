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
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;
        private IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetOrderById(int id)
        {
            Order order = _orderService.GetOrderInfo(id);

            if(order == null)
            {
                return NotFound(id);
            }

            return Ok(order);
        }


        [HttpPost]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult CreateOrder([FromBody] CreateOrderDto dto)
        {
            Order order = _mapper.Map<Order>(dto);

            Order newOrder = _orderService.RegisterNewOrder(order);

            if(newOrder == null)
            {
                throw new ApplicationException("Não foi possível criar um novo registro de estoque!");
            }

            return CreatedAtAction(
                nameof(GetOrderById),
                new { id = newOrder.Id },
                newOrder
            );
        }
    }
}
