using Microsoft.AspNetCore.Mvc;
using Store.Repositories;
using Store.Infrastructure.DTO;
using Store.Repositories.IRepository;
using Store.DomainEntities.DBEntities;
using Org.BouncyCastle.Utilities;
using Store.Repositories.Repository;
using Store.DomainEntities;

namespace Store.API.Controllers.Order
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly OrderIRepository _orderIrepository;
        private readonly ProductIRepository _productIRepository;
        private readonly CustomerIRepository _customerIRepository;
        public OrderController(OrderIRepository orderIrepository, ProductIRepository productIRepository ,CustomerIRepository customerIRepository)
        {
            _orderIrepository = orderIrepository;
            _productIRepository = productIRepository;
            _customerIRepository = customerIRepository;
        }
        [HttpPost("AddNewOrder")]

        public IActionResult AddNewOrder(Store.Infrastructure.DTO.Order orderDTO)
        {
            try
            {
                Store.DomainEntities.DBEntities.Order order = new DomainEntities.DBEntities.Order();

                order.OrderId = orderDTO.OrderId;
                order.ProductId = orderDTO.ProductId;
                order.CustomerId = orderDTO.CustomerId;
                order.TotalAmount = orderDTO.TotalAmount;
                order.Quantity = orderDTO.Quantity;
                order.OrderDate = orderDTO.OrderDate.Date;
                order.Status = orderDTO.Status = "Waiting";

                _orderIrepository.Add(order);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error {ex.Message}");
            }
        }

  
        [HttpPost("PlaceOrder")]
        public IActionResult PlaceOrder([FromBody] Store.Infrastructure.DTO.Order orderDTO)
        {
            try
            {
                var product = _productIRepository.Find(p => p.ProductId == orderDTO.ProductId).FirstOrDefault();
                if (product == null)
                {
                    return BadRequest("Product not found");
                }

                var customer = _customerIRepository.Find(c => c.CustomerId == orderDTO.CustomerId).FirstOrDefault();
                if (customer == null)
                {
                    return BadRequest("Customer not found");
                }

                var order = new Store.DomainEntities.DBEntities.Order
                {
                    ProductId = product.ProductId,
                    CustomerId = customer.CustomerId,
                    Quantity = orderDTO.Quantity,
                    TotalAmount = product.Price * orderDTO.Quantity, // Calculate TotalAmount as Quantity * Price
                    OrderDate = DateTime.Now,
                    Status = "Waiting"
                };

                _orderIrepository.Add(order);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error {ex.Message}");
            }
        }


        [HttpGet("GetOrderById")]
        public IActionResult GetOrderById(int id)
        {
            try
            {
               Store.DomainEntities.DBEntities.Order order = new DomainEntities.DBEntities.Order();
                order = _orderIrepository.GetById(id);
                Infrastructure.DTO.Order orderDTO = new Infrastructure.DTO.Order();

                orderDTO.OrderId = order.OrderId;
                orderDTO.ProductId = order.ProductId;
                orderDTO.CustomerId = order.CustomerId;
                orderDTO.TotalAmount = order.TotalAmount;
                orderDTO.Quantity = order.Quantity;
                orderDTO.OrderDate = order.OrderDate;
                orderDTO.Status = order.Status;

                return Ok(orderDTO);

            }
            catch (Exception ex)
            {
                return BadRequest($"Error {ex.Message}");
            }
        }
    


        [HttpGet("GetAllOrder")]
        public IActionResult GetAllOrder()
        {
            try
            {
                // Fetch all orders without joining with customer or product
                var result = _orderIrepository.GetAll()
                    .Select(obj => new
                    {
                        OrderId = obj.OrderId,
                        TotalAmount = obj.TotalAmount,
                        Quantity = obj.Quantity,
                        OrderDate = obj.OrderDate,
                        Status = obj.Status,
                        CustomerId = obj.CustomerId,
                        productId = obj.ProductId,

                    }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error {ex.Message}");
            }
        }

        [HttpGet("GetOrder")]
        public IActionResult GetOrder(int id)
        {
            try
            {
                var order = _orderIrepository.Find(c => c.CustomerId == id).ToList();

                if (order == null)
                {
                    return NotFound();
                }

                return Ok(order);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("DeleteOrder")]
        public IActionResult DeleteOrder(int id)
        {
            DomainEntities.DBEntities.Order order = new DomainEntities.DBEntities.Order();
            order = _orderIrepository.GetById(id);
            _orderIrepository.Delete(order);

            return Ok(order);

        }

        [HttpPost("UpdateOrderUser")]
        public IActionResult UpdateOrderUser(Infrastructure.DTO.Order orderDTO)
        {
            DomainEntities.DBEntities.Order order = new DomainEntities.DBEntities.Order();
            DomainEntities.DBEntities.Product product = new DomainEntities.DBEntities.Product();

            try
            {
                product = _productIRepository.GetById(orderDTO.ProductId);
                order = _orderIrepository.GetById(orderDTO.OrderId);
                order.Quantity = orderDTO.Quantity;
                order.TotalAmount = product.Price * order.Quantity;
                _orderIrepository.Update(order);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("Fail");
            }
        }



        [HttpPost("UpdateOrderAdmin")]
        public IActionResult UpdateOrderAdmin(Infrastructure.DTO.Order orderDTO)
        {
            DomainEntities.DBEntities.Order order = new DomainEntities.DBEntities.Order();
            try
            {
                order = _orderIrepository.GetById(orderDTO.OrderId);

                order.Status = orderDTO.Status;

                _orderIrepository.Update(order);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("Fail");
            }
        }

        //Reprot frome company 

        //[HttpGet("GetAllOrders")]
        //public IActionResult GetAllOrders()
        //{
        //    using (var context = new StoreSystemContext()) // Replace 'StoreSystemContext' with your actual DbContext name
        //    {
        //        var result = from order in context.Orders
        //                     join product in context.Products
        //                     on order.ProductId equals product.ProductId
        //                     join customer in context.Customers
        //                     on order.CustomerId equals customer.CustomerId
        //                     group new { order, product} by new { product.ProductId, product.Name, product.Price } into grouped
        //                     select new OrderProductDTO
        //                     {
        //                         ProductId = grouped.Key.ProductId,
        //                         ProductName = grouped.Key.Name,
        //                         Quantity = grouped.Sum(x => x.order.Quantity),
        //                         TotalAmount = (decimal)grouped.Sum(x => x.order.Quantity * grouped.Key.Price) // Quantity * Price for TotalAmount
        //                     };



        //        return Ok(result.ToList());
        //    }
        //}

        //Report from admin update 
        //And its vary important 
        [HttpGet("GetAllOrders")]
        public IActionResult GetAllOrders(DateTime? startDate, DateTime? endDate)
        {
            using (var context = new StoreSystemContext())
            {
                var query = from order in context.Orders
                            join product in context.Products on order.ProductId equals product.ProductId
                            join customer in context.Customers on order.CustomerId equals customer.CustomerId
                            where (!startDate.HasValue || order.OrderDate >= startDate.Value) &&
                                  (!endDate.HasValue || order.OrderDate <= endDate.Value)
                            group new { order, product } by new { product.ProductId, product.Name, product.Price } into grouped
                            select new OrderProductDTO
                            {
                                ProductId = grouped.Key.ProductId,
                                ProductName = grouped.Key.Name,
                                Quantity = grouped.Sum(x => x.order.Quantity),
                                TotalAmount = (decimal)grouped.Sum(x => x.order.Quantity * grouped.Key.Price),
                                OrderDate = grouped.Max(x => x.order.OrderDate) // Get the most recent order date
                            };

                return Ok(query.ToList());
            }
        }


            //Report From user 

            [HttpGet("GetAllOrderUser")]

        public IActionResult GetAllOrderUser(int id)
        {
            using (var context = new StoreSystemContext()) // Replace 'StoreSystemContext' with your actual DbContext name
            {
                var result = from order in context.Orders
                             join product in context.Products
                             on order.ProductId equals product.ProductId
                             join customer in context.Customers
                             on order.CustomerId equals customer.CustomerId
                             where order.CustomerId == id
                             group new { order, product, customer } by new
                             {
                                 customer.CustomerId,
                                 customer.Name,
                                 customer.Telephone,
                                 customer.Address
                             } into customerGroup
                             select new CustomerOrderDTO
                             {
                                 Name = customerGroup.Key.Name,
                                 Telephone = customerGroup.Key.Telephone,
                                 Address = customerGroup.Key.Address,
                                 Products = customerGroup
                                     .GroupBy(x => new
                                     {
                                         x.product.ProductId,
                                         x.product.Name,
                                         x.product.Price
                                     })
                                     .Select(g => new OrderProductDTO
                                     {
                                         ProductId = g.Key.ProductId,
                                         ProductName = g.Key.Name,
                                         Quantity = g.Sum(x => x.order.Quantity),
                                         TotalAmount = (decimal)g.Sum(x => x.order.Quantity * g.Key.Price) // Quantity * Price for TotalAmount
                                     })
                                     .ToList()
                             };

                return Ok(result.ToList());
            }
        }







            [HttpPost("Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
