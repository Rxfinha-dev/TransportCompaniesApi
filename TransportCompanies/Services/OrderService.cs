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

    public bool UpdateStatus(int id, Order orderToUpdate)
    {

        var order = _orderRepository.GetOrderToUpdate(id, true);

        if (order is null)
            throw new Exception("Pedido não encontrado");

        if (!order.IsDispatched)
            order.IsDispatched = orderToUpdate.IsDispatched;

        order.statusID = orderToUpdate.statusID;



        return _orderRepository.Save();

    }

    public async Task<bool> UpdateAddresses(int id, UpdateAdressDto addressToUpdate)
    {
       

        var order = _orderRepository.GetOrderToUpdate(id, true);

        if (order.IsDispatched)
            throw new Exception("O pedido já foi despachado");

        if (order == null)
            throw new Exception("Pedido não encontrado");
        
        order.Origin = await GetAddressByCep(addressToUpdate.Origin);
        order.Destination = await GetAddressByCep(addressToUpdate.Destination);

        return _orderRepository.Save();
    }

    public ICollection<Order> GetOrders()
    {
        return _orderRepository.GetOrders();
    }

    public Order GetOrder(int id)
    {
        if(!_orderRepository.OrderExists(id))
            return null;

        return _orderRepository.GetOrderById(id);            
    }

    public async Task<bool> CreateOrder(Order order)
    {

       order.Origin = await GetAddressByCep(order.Origin);
       order.Destination = await GetAddressByCep(order.Destination);

        return _orderRepository.CreateOrder(order);
            
    }

  
    public bool UpdateClientOrder(int id, Order orderToUpdate)
    {
        var order = _orderRepository.GetOrderToUpdate(id, true);

        if (order is null)
            return false;
        
        if (order.IsDispatched)
            throw new Exception("O pedido já foi despachado");

    

        if (!_costumerRepository.CostumerExists(orderToUpdate.costumerId))
            throw new Exception("O cliente não existe");


        order.costumerId = orderToUpdate.costumerId;
       


        return _orderRepository.UpdateOrder(order);
    }

    public bool UpdateOrderItens(int id, Order orderToUpdate)
    {

       
        var order = _orderRepository.GetOrderToUpdate(id,true);

        if (orderToUpdate.orderedItens is null)
            throw new Exception("items is null");

        if (order is null)
            throw new Exception("order is null on load");

        if (order.IsDispatched is true)
            throw new Exception("Pedido já foi despachado");


        order.orderedItens = orderToUpdate.orderedItens;

        return _orderRepository.UpdateOrder(order);
       
    }

    public bool OrderExists(int id)
    {
        return _orderRepository.OrderExists(id);
    }

    public bool DeleteOrder(int id)
    {
        if (!_orderRepository.OrderExists(id))
            return false;
        
        var orderToDelete = _context.Orders.Where(o=>o.Id == id).FirstOrDefault();

        return _orderRepository.DeleteOrder(orderToDelete);
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
