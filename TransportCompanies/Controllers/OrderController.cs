using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TransportCompanies.DTO;
using TransportCompanies.Interfaces.IServices;
using TransportCompanies.Models;

namespace TransportCompanies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Order>))]
        public async Task<IActionResult> GetOrdersAsync()
        {
            var orders = _mapper.Map<List<OrderDto>>(await _orderService.GetOrdersAsync());

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orders);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Order))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetOrderAsync(int id)
        {
            if(!await _orderService.OrderExistsAsync(id))
                return NotFound();  

            var order = await _orderService.GetOrderAsync(id);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(order);                 

        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public async Task<IActionResult> CreateOrder([FromBody]OrderDto orderCreate)
        {
            if(orderCreate is null)
                return BadRequest(ModelState);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderMap = _mapper.Map<Order>(orderCreate);

            if(!await _orderService.CreateOrder(orderMap))
            {
                ModelState.AddModelError("", "Algo deu errado ao criar o Pedido");
                return StatusCode(500, ModelState);
            }

            return Ok("Criado Com Sucesso");
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateClientOrderAsync(int id, OrderDto orderToUpdate)
        {
            if (id != orderToUpdate.Id)
                return BadRequest("Os id's nao coincidem");

            if (!await _orderService.OrderExistsAsync(id))
                return NotFound();
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderMap = _mapper.Map<Order>(orderToUpdate);

            if(!await _orderService.UpdateClientOrderAsync(id, orderMap))
            {
                ModelState.AddModelError("", "Algo deu errado ao Atualizar o cliente q fez o pedido");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpPut("{id}/items")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateOrderItensAsync(int id, OrderDto orderToUpdate)
        {
            if (id != orderToUpdate.Id)
                return BadRequest("Os id's nao coincidem");

            if (!await _orderService.OrderExistsAsync(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderMap = _mapper.Map<Order>(orderToUpdate);

            if (!await _orderService.UpdateOrderItensAsync(id, orderMap))
            {
                ModelState.AddModelError("", "Algo deu errado ao Atualizar os itens do pedido");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpPatch("{id}/status")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]

        public async Task<IActionResult> UpdateOrderStatusAsync(int id, OrderDto orderToUpdate)
        {
            if (id != orderToUpdate.Id)
                return BadRequest("Os id's nao coincidem");

            if (!await _orderService.OrderExistsAsync(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderMap = _mapper.Map<Order>(orderToUpdate);

            if (!await _orderService.UpdateStatusAsync(id, orderMap))
            {
                ModelState.AddModelError("", "Algo deu errado ao Atualizar o status do pedido");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpPatch("{id}/addresses")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateAddressAsync(int id, [FromBody] UpdateAdressDto addressToUpdate)
        {
            if (addressToUpdate == null)
                return BadRequest("Objeto inválido.");

            if (!await _orderService.OrderExistsAsync(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _orderService.UpdateAddresses(id, addressToUpdate))
            {
                ModelState.AddModelError("", "Algo deu errado ao atualizar o endereço");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteOrderAsync(int id)
        {
            if (!await _orderService.OrderExistsAsync(id))
                return NotFound();           
           

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _orderService.DeleteOrderAsync(id))
            {
                ModelState.AddModelError("", "Algo deu errado ao deletar");
            }

            return NoContent();
            
        }


    }
}
