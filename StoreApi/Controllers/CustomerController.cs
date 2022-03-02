using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BL;
using StoreModel;
using StoreDL;
using System.Data.SqlClient;
using StoreApi.DataTransferObject;

namespace StoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private IStoreBL _storeBL;
        public CustomerController(IStoreBL p_storeBL)
        {
            _storeBL = p_storeBL;
        }

        //=====================================================================================================================================


        [HttpPost("PlaceOrder")] // 5. Place Orders to a location
        public IActionResult PlaceOrder([FromBody] Order p_order)
        {
            try
            {
                Log.Information("Customer placed an order.");
                _storeBL.PlaceOrder(p_order);
                return Created("Placed Order successfully!", p_order);
            }
            catch (System.Exception e)
            {

                return StatusCode(500, e);
            }
        }

        //=====================================================================================================================================

        [HttpGet("GetCustomers")] // Get All Customers
        public IActionResult GetAllCustomers()
        {
            try
            {
                return Ok(_storeBL.GetAllCustomers());
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }

        //=====================================================================================================================================

        [HttpGet("{p_custid}")] // Get Customer by Customer ID
        public IActionResult GetCustomerByID(int p_custid)
        {
            try
            {
                Log.Information("User seached customers by ID");
                return Ok(_storeBL.GetAllCustomers().Find(p => p.ID.Equals(p_custid))); //why it did not initially work on swagger is bc parameters were not added
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }

        //=====================================================================================================================================

        [HttpGet("Orders/{p_custid}")] // Get Order History of Customer
        public IActionResult GetCustomerOrderByID(int p_custid, string p_orderby)
        {
            try
            {
                //p_orderby.ToLower();

                List<OrderDetails> _listOrderDetail = new List<OrderDetails>();
                List<OrderDetails> _sortedOrdersList = new List<OrderDetails>();
                List<Order> _listCustomerOrder = _storeBL.GetAllOrders().FindAll(p => p.CustomerID.Equals(p_custid));

                foreach (var item in _listCustomerOrder)
                {
                    _listOrderDetail.Add(new OrderDetails()
                    {
                        OrderID = item.OrderID,
                        CustomerID = item.CustomerID,
                        CustomerDetail = new CustomerDetail()
                        {
                            Name = _storeBL.GetAllCustomers().Find(p => p.ID.Equals(item.CustomerID)).Name,
                            Address = _storeBL.GetAllCustomers().Find(p => p.ID.Equals(item.CustomerID)).Address,
                            Phone = _storeBL.GetAllCustomers().Find(p => p.ID.Equals(item.CustomerID)).Phone
                        },
                        StoreID = item.StoreID,
                        StoreDetail = new StoreFrontDetails()
                        {
                            StoreFrontName = _storeBL.GetAllStores().Find(p => p.StoreID.Equals(item.StoreID)).StoreFrontName,
                            StoreFrontAddress = _storeBL.GetAllStores().Find(p => p.StoreID.Equals(item.StoreID)).StoreFrontAddress
                        },
                        TotalPrice = item.TotalPrice,
                        DateCreated = item.DateCreated,
                        Cart = _storeBL.GetAllCartByOrderID(item.OrderID)
                    });
                }

                switch (p_orderby.ToLower())
                {
                    case "date":
                        Log.Information("User looked at orders history by date created");
                        return Ok(_sortedOrdersList = _listOrderDetail.OrderBy(p => p.DateCreated).ToList());
                        break;
                    case "totalprice":
                        Log.Information("User looked at order history by total price");
                        return Ok(_sortedOrdersList = _listOrderDetail.OrderBy(p => p.TotalPrice).ToList());
                        break;
                    default:
                        Log.Information("User looked at order history");
                        return Ok(_listOrderDetail);
                        break;
                }

            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }

        //=====================================================================================================================================
    }
}
