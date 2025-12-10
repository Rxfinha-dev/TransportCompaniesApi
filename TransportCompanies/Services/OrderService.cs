using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using TransportCompanies.Data;
using TransportCompanies.DTO;
using TransportCompanies.Interfaces.IRepository;
using TransportCompanies.Interfaces.IServices;
using TransportCompanies.Models;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICostumerRepository _costumerRepository;
    private readonly HttpClient _client;
    private readonly DataContext _context;

    public OrderService(IOrderRepository orderRepository, IHttpClientFactory clientFactory, ICostumerRepository costumerRepository, DataContext context)
    {
        _orderRepository = orderRepository;
        _client = clientFactory.CreateClient("ViaCep");
        _costumerRepository = costumerRepository;
        _context = context;
    }

    
    public async Task<bool> UpdateStatusAsync(int id, Order orderToUpdate)
    {

        var order = await _orderRepository.GetOrderToUpdate(id, true);

        if (order is null)
            throw new Exception("Pedido não encontrado");

        if (!order.IsDispatched)
            order.IsDispatched = orderToUpdate.IsDispatched;

        order.statusID = orderToUpdate.statusID;



        return await _orderRepository.Save();

    }

    public async Task<bool> UpdateAddresses(int id, UpdateAdressDto addressToUpdate)
    {
       

        var order = await _orderRepository.GetOrderToUpdate(id, true);

        if (order.IsDispatched)
            throw new Exception("O pedido já foi despachado");

        if (order == null)
            throw new Exception("Pedido não encontrado");
        
        order.Origin = await GetAddressByCep(addressToUpdate.Origin);
        order.Destination = await GetAddressByCep(addressToUpdate.Destination);

        return await _orderRepository.Save();
    }

    public async Task<ICollection<Order>> GetOrdersAsync()
    {
        return await _orderRepository.GetOrders();
    }

    public async Task<Order> GetOrderAsync(int id)
    {
        if(!await _orderRepository.OrderExists(id))
            return null;

        return await _orderRepository.GetOrderById(id);            
    }

    public async Task<bool> CreateOrder(Order order)
    {

       order.Origin = await GetAddressByCep(order.Origin);
       order.Destination = await GetAddressByCep(order.Destination);

        return await _orderRepository.CreateOrder(order);
            
    }

  
    public async Task<bool> UpdateClientOrderAsync(int id, Order orderToUpdate)
    {
        var order = await _orderRepository.GetOrderToUpdate(id, true);

        if (order is null)
            return false;
        
        if (order.IsDispatched)
            throw new Exception("O pedido já foi despachado");

    

        if (! await _costumerRepository.CostumerExists(orderToUpdate.costumerId))
            throw new Exception("O cliente não existe");


        order.costumerId = orderToUpdate.costumerId;
       


        return await _orderRepository.UpdateOrder(order);
    }

    public async Task<bool> UpdateOrderItensAsync(int id, Order orderToUpdate)
    {

       
        var order = await _orderRepository.GetOrderToUpdate(id,true);

        if (orderToUpdate.orderedItens is null)
            throw new Exception("items is null");

        if (order is null)
            throw new Exception("order is null on load");

        if (order.IsDispatched is true)
            throw new Exception("Pedido já foi despachado");


        order.orderedItens = orderToUpdate.orderedItens;

        return await _orderRepository.UpdateOrder(order);
       
    }

    public async Task<bool> OrderExistsAsync(int id)
    {
        return await _orderRepository.OrderExists(id);
    }

    public async Task<bool> DeleteOrderAsync(int id)
    {
        if (!await _orderRepository.OrderExists(id))
            return false;
        
        var orderToDelete = _context.Orders.Where(o=>o.Id == id).FirstOrDefault();

        return await _orderRepository.DeleteOrder(orderToDelete);
    }

   

    public async Task<bool> IsCepValid(string cep)
    {
        var response = await _client.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
        if (!response.IsSuccessStatusCode)
            return false;

        var json = await response.Content.ReadAsStringAsync();

        return json.Contains("\"erro\"") ? false : true;
    }

    public async Task<AddressDto> GetAddressByCep(AddressDto addressDto)
    {
        var response = await _client.GetAsync($"https://viacep.com.br/ws/{addressDto.Cep}/json/");

        if (!response.IsSuccessStatusCode)
            throw new Exception("CEP inválido");

        var address = await response.Content.ReadFromJsonAsync<ViaCepResponse>();

        if (address is null || address.erro is true)
            throw new Exception("Erro ao obter endereço!");

        addressDto.Rua = address.logradouro;
        addressDto.Bairro = address.bairro;
        addressDto.Cidade = address.localidade;
        addressDto.Estado = address.uf;

        return addressDto;
    }


}
