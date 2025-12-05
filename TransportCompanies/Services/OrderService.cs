using System.Runtime.ConstrainedExecution;
using TransportCompanies.DTO;
using TransportCompanies.Interfaces.IRepository;
using TransportCompanies.Interfaces.IServices;
using TransportCompanies.Models;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly HttpClient _client;

    public OrderService(IOrderRepository orderRepository, IHttpClientFactory clientFactory)
    {
        _orderRepository = orderRepository;
        _client = clientFactory.CreateClient("ViaCep");
    }

    public bool UpdateStatus(int id, Order orderToUpdate)
    {

        var order = _orderRepository.GetOrder(id);

        if (order == null)
            return false;

        order.Status = orderToUpdate.Status;

        return _orderRepository.UpdateOrder(order);

    }

    public bool UpdateAddresses(int id, AddressDto origin, AddressDto destination)
    {
        var order = _orderRepository.GetOrder(id);

        if (order == null)
            return false;

      
        

        order.Origin = origin;
        order.Destination = destination;

        return _orderRepository.UpdateOrder(order);
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

    public bool CreateOrder(Order order)
    {
        return false;
    }

    public bool UpdateClientOrder(int id, Order orderToUpdate)
    {
        var order = _orderRepository.GetOrder(id);

        if (order == null)
            return false;

        order.Costumer = orderToUpdate.Costumer;

        return _orderRepository.UpdateOrder(order);
    }

    public bool UpdateOrderItens(int id, Order orderToUpdate)
    {

       
        var order = _orderRepository.GetOrder(id);

        if (order == null)
            return false;

        if(order.Status.IsDispatched == true)
            return false;

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

   

    public async Task<bool> IsOriginCepValid(Order order)
    {
        var response = await _client.GetAsync($"https://viacep.com.br/ws/{order.Origin.Cep}/json/");
        if (!response.IsSuccessStatusCode)
            return false;

        var json = await response.Content.ReadAsStringAsync();

        return json.Contains("\"erro\"") ? false : true;
    }

    public Task<bool> IsDestinationCepValid(Order order)
    {
        throw new NotImplementedException();
    }
}
