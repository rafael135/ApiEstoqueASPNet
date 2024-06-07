using ApiEstoqueASP.Models;
using ApiEstoqueASP.Repositories.Interfaces;
using ApiEstoqueASP.Services.Interfaces;

namespace ApiEstoqueASP.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
        }

        public OrderItem? GetOrderItemById(int id)
        {
            OrderItem? orderItem = _orderItemRepository.GetOrderItemById(id);

            return orderItem;
        }

        public List<OrderItem> GetOrderItems(Order order)
        {
            throw new NotImplementedException();
        }

        public OrderItem RegisterNewOrderItem(OrderItem orderItem)
        {
            return _orderItemRepository.Add(orderItem);
            //return _orderItemRepository.GetOrderItemById(orderItem.Id);
        }

        public Order RegisterNewOrder(Order order)
        {
            // Registra a encomenda no BD
            _orderRepository.Add(order);

            // Pega os items pendentes da encomenda
            List<OrderItem> pendingOrderItems = _orderItemRepository
                .Find(or => or.OrderId == null && or.UserId == order.UserId)
                .ToList();

            // Percorre cada um deles e seta o id de ordem da encomenda e atualiza a quantidade de produtos restantes
            for (int i = 0; i < pendingOrderItems.Count; i++)
            {
                pendingOrderItems[i].OrderId = order.Id;
                pendingOrderItems[i].Product.InStock = pendingOrderItems[i].Product.InStock - pendingOrderItems[i].Quantity;
            }

            _orderItemRepository.UpdateRange(pendingOrderItems);

            double sum = pendingOrderItems.Sum(or => or.Quantity * or.Product.Price);
            
            order.TotalPrice = sum;
            _orderRepository.Update(order);

            return order;
        }

        public Order GetOrderInfo(int id)
        {
            Order order = _orderRepository.GetById(id);

            return order;
        }
    }
}
