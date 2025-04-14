using DTOs.DTO.Order;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.core.Interfaces;
using Shipping.core.Models;
using Shipping.services;


namespace Shipping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepo _orderRepo;
        private readonly OrderService _orderService;

        public OrderController(IOrderRepo orderRepo, OrderService orderService)
        {

            _orderRepo = orderRepo;
            _orderService = orderService;
        }

        public IOrderRepo OrderRepo { get; }

        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetAll(string? id,string? role,int pagesize, int pagenum , DTOs.DTO.Order.OrderStatus? status = null )
        {
            var orderss = await _orderRepo.GetAllOrders();
            if (orderss == null || !orderss.Any())
            {
                return NotFound("No orders found.");
            }
           
            var orders = orderss.Where(o => !o.isDeleted).ToList();

            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(role))
            {
                if (role == "Merchant")
                {
                    orders = orders.Where(o => o.MerchantId == id).ToList();
                }
                else if (role == "Representative")
                {
                    orders = orders.Where(o => o.RepresentativeId == id).ToList();
                }
            }

            if (status == null)
            {
                  orders = orders.Where(o => o.isDeleted == false  ).ToList();
            }
            else
            {
                  orders = orders.Where(o => o.isDeleted == false && (int)o.orderStatus == (int)status.Value).ToList();
            }
            // Apply pagination after filtering
            var paginatedOrders = orders
                .Skip((pagenum - 1) * pagesize)
                .Take(pagesize)
                .ToList();
            var orderDTOs = paginatedOrders.Select(o => new GetOrderDTO
            {
                SerialNum = o.Id,
                ClientName = o.ClientName,
                PhoneNumber = o.FirstPhoneNumber,
                orderCost = o.ProductTotalCost,
                Governorate = o.Governorate.Name,
                City = o.City.Name,
                Date = o.Date
            }).ToList();


            return Ok(orderDTOs);
        }
       
        [HttpGet("GetOrderById/{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderRepo.GetOrderById(id);
            if (order == null)
            {
                return NotFound("Order not found.");
            }
            var orderDTO = new GetOrderDTO
            {
                SerialNum = order.Id,
                ClientName = order.ClientName,
                PhoneNumber = order.FirstPhoneNumber,
                orderCost = order.ProductTotalCost,
                Governorate = order.Governorate.Name,
                City = order.City.Name,
                Date = order.Date
            };
            return Ok(orderDTO);
        }
        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] ADDOrderDTO order)
        {
            if (ModelState.IsValid)
            {
                await _orderService.CreateOrder(order);
                return Ok(new { message = "Order Created Successfully" });

            }
            else
            {
                return BadRequest(ModelState);

            }
        }
        [HttpPut("UpdateOrder/{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] ADDOrderDTO order)
        {
            if (ModelState.IsValid)
            {
                var existingOrder = await _orderRepo.GetOrderById(id);
                if (existingOrder == null)
                {
                    return NotFound("Order not found.");
                }
                await _orderService.UpdateOrder(id, order);
                return Ok(new { message = "Order Updated Successfully" });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        [HttpDelete("DeleteOrder/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _orderRepo.GetOrderById(id);
            if (order == null)
            {
                return NotFound("Order not found.");
            }
            await _orderService.DeleteOrder(id);
            return Ok(new { message = "Order Deleted Successfully" });
        }

        [HttpGet("GetOrderReport")]
        public async Task<ActionResult<ReportDTO>> GetOrderReport(int pageSize, int pageNum, DateTime? fromDate = null, DateTime? toDate = null, DTOs.DTO.Order.OrderStatus? status = null)
        {
            var report = await _orderService.GetOrderReport(pageSize, pageNum ,fromDate ,toDate,status);
            if (report == null || !report.Any())
            {
                return NotFound("No orders found.");
            }
            return Ok(report);
        }
    }
}

