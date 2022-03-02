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
    [Route("api/[controller]")] //This is used to configure the endpoints of all the actions inside of this controller
    [ApiController]
    public class StoreController : ControllerBase
    {
        private IStoreBL _storeBL;
        public StoreController(IStoreBL p_storeBL)
        {
            _storeBL = p_storeBL;
        }

        //=====================================================================================================================================


        [HttpGet("SearchCustomerByName/{p_name}")]//"GetAll" is the end point (shows up in swagger next to GET)
        public IActionResult SearchCustomerByName(string p_name) //action is a method inside of a controller
        {
            try
            {
                Log.Information("User Searched Customer by name" + p_name);
                return Ok(_storeBL.SearchCustomers(p_name.ToLower()));


            }
            catch (SqlException)
            {
                //Api is responsible for sending the right resource and the right status code
                //in this case if it was unable to connect to the db it will give a 404 status code

                return NotFound(); //It can be broke but if 0k method is used it will still return a 200 status code.
            }

            //return Ok(_storeBL.GetAllCustomers());//Ok method gives 200 status code. Developer's job to give status codes.
        }

        //=====================================================================================================================================

        [HttpGet("Stores/{p_storeID}")]
        public IActionResult GetStoreByStoreID(int p_storeID)
        {
            try
            {
                Log.Information("User Searched Store by StoreID");
                StoreFront _store = _storeBL.GetAllStores().Find(p => p.StoreID.Equals(p_storeID));
                StoreFrontDetails data = new StoreFrontDetails();
                data.StoreFrontName = _store.StoreFrontName;
                data.StoreFrontAddress = _store.StoreFrontAddress;
                return Ok(new { id = p_storeID, data });
            }
            catch (System.Exception e)
            {
                return NotFound(e.Message);
            }
        }

        //=====================================================================================================================================

        [HttpGet("StoresInventory/{p_storeID}")]
        public IActionResult GetStoreInventoryByStoreID(int p_storeID)
        {
            try
            {
                Log.Information("Store Looked at Inventory of store # " + p_storeID);
                return Ok(_storeBL.GetAllInventoryByStoreID(p_storeID));
            }
            catch (System.Exception e)
            {
                return NotFound(e.Message);
            }
        }

        //=====================================================================================================================================


        [HttpPut("Replenish Inventory")]

        public IActionResult ReplenishInventory([FromBody] Inventory p_inventory)
        {
            _storeBL.ReplenishInventory(p_inventory);
            Log.Information("User replenished " + p_inventory + " of product #: " + p_inventory.ProductID + " to the inventory!");

            return Created("Sucessfully replenished Inventory!", p_inventory);
        }

        //=====================================================================================================================================

        [HttpGet("Orders/{p_storeid}")] // 6. View Order History of Store
        public IActionResult GetStoreOrderHistoryByID(int p_storeid, string p_orderby)
        {
            try
            {
                //p_orderby.ToLower();

                List<OrderDetails> _listOrderDetail = new List<OrderDetails>();
                List<OrderDetails> _sortedOrdersList = new List<OrderDetails>();
                List<Order> _listStoreOrder = _storeBL.GetAllOrders().FindAll(p => p.StoreID.Equals(p_storeid));

                foreach (var item in _listStoreOrder)
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
            catch (System.Exception e)
            {
                return NotFound(e.Message);
            }
        }

        //=====================================================================================================================================

    }
}
