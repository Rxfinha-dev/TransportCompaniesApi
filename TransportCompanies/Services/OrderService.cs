using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using TransportCompanies.DTO;
using TransportCompanies.Interfaces.IRepository;
using TransportCompanies.Interfaces.IServices;
using TransportCompanies.Models;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICostumerRepository _costumerRepository;
    private readonly HttpClient _client;

    public OrderService(IOrderRepository orderRepository, IHttpClientFactory clientFactory, ICostumerRepository costumerRepository)
    {
        _orderRepository = orderRepository;
        _client = clientFactory.CreateClient("ViaCep");
        _costumerRepository = costumerRepository;
    }

    public bool UpdateStatus(int id, Order orderToUpdate)
    {

        var order = _orderRepository.GetOrder(id);

        if (order is null)
            throw new Exception("Pedido não encontrado");

        if (!order.IsDispatched)
            order.IsDispatched = orderToUpdate.IsDispatched;

        order.statusID = orderToUpdate.statusID;



        return _orderRepository.Save();

    }

    public async Task<bool> UpdateAddresses(int id, UpdateAdressDto addressToUpdate)
    {
       

        var order = _orderRepository.GetOrder(id);

        if (order.IsDispatched)
            throw new Exception("O pedido já foi despachado");

        if (order == null)
            return false;

        if (!await IsCepValid(addressToUpdate.Origin.Cep))
            throw new Exception("Cep de origem inválido");

        if (!await IsCepValid(addressToUpdate.Origin.Cep))
            throw new Exception("Cep de destino inválido");



        order.Origin = addressToUpdate.Origin;
        order.Destination = addressToUpdate.Destination;

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

        return _orderRepository.GetOrder(id);            
    }

    public async Task<bool> CreateOrder(Order order)
    {

        if (!await IsCepValid(order.Origin.Cep))
            throw new Exception("Cep de origem inválido");
        if(!await IsCepValid(order.Destination.Cep))
        throw new Exception("Cep de destino inválido");

        return _orderRepository.CreateOrder(order);
            
    }

  
    public bool UpdateClientOrder(int id, Order orderToUpdate)
    {
        var order = _orderRepository.GetOrder(id);

        if (order is null)
            return false;

        if (!_costumerRepository.CostumerExists(orderToUpdate.costumerId))
            throw new Exception("O cliente não existe");


        order.costumerId = orderToUpdate.costumerId;
       


        return _orderRepository.UpdateOrder(order);
    }

    public bool UpdateOrderItens(int id, Order orderToUpdate)
    {

       
        var order = _orderRepository.GetOrder(id);

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

    public bool DeleteOrder(Order order)
    {
        if (!_orderRepository.OrderExists(order.Id))
            return false;

        return _orderRepository.DeleteOrder(order);
    }

   

    public async Task<bool> IsCepValid(string cep)
    {
        var response = await _client.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
        if (!response.IsSuccessStatusCode)
            return false;

        var json = await response.Content.ReadAsStringAsync();

        return json.Contains("\"erro\"") ? false : true;
    }


}
