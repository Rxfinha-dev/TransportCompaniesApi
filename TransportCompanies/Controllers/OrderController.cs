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
        public IActionResult GetOrders()
        {
            var orders = _mapper.Map<List<OrderDto>>(_orderService.GetOrders());

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orders);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Order))]
        [ProducesResponseType(400)]
        public IActionResult GetOrder(int id)
        {
            if(!_orderService.OrderExists(id))
                return NotFound();  

            var order = _orderService.GetOrder(id);

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
        public IActionResult UpdateClientOrder(int id, OrderDto orderToUpdate)
        {
            if (id != orderToUpdate.Id)
                return BadRequest("Os id's nao coincidem");

            if (!_orderService.OrderExists(id))
                return NotFound();
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderMap = _mapper.Map<Order>(orderToUpdate);

            if(!_orderService.UpdateClientOrder(id, orderMap))
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
        public IActionResult UpdateOrderItens(int id, OrderDto orderToUpdate)
        {
            if (id != orderToUpdate.Id)
                return BadRequest("Os id's nao coincidem");

            if (!_orderService.OrderExists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderMap = _mapper.Map<Order>(orderToUpdate);

            if (!_orderService.UpdateOrderItens(id, orderMap))
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

        public IActionResult UpdateOrderStatus(int id, OrderDto orderToUpdate)
        {
            if (id != orderToUpdate.Id)
                return BadRequest("Os id's nao coincidem");

            if (!_orderService.OrderExists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderMap = _mapper.Map<Order>(orderToUpdate);

            if (!_orderService.UpdateStatus(id, orderMap))
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

            if (!_orderService.OrderExists(id))
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
        public IActionResult DeleteOrder(int id)
        {
            if (!_orderService.OrderExists(id))
                return NotFound();           
           

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_orderService.DeleteOrder(id))
            {
                ModelState.AddModelError("", "Algo deu errado ao deletar");
            }

            return NoContent();
            
        }


    }
}
