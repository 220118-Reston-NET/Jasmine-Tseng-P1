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

        [HttpPost("New Customer Registration")]// 1. add a new customer
        public IActionResult Register([FromBody] CustomerRegisterForm p_customer)
        {
            try
            {
                //Check all the fills are coorect
                if (string.IsNullOrWhiteSpace(p_customer.UserName))
                {
                    return BadRequest(new { Result = "Your username should not be empty nor have blank spaces!" });
                }
                if (string.IsNullOrWhiteSpace(p_customer.Password) || p_customer.Password.Length < 6)
                {
                    return BadRequest(new { Result = "Your password should not be empty nor have blank spaces!" });
                }
                if (string.IsNullOrWhiteSpace(p_customer.Name))
                {
                    return BadRequest(new { Result = "Your name should not be empty nor have blank spaces!" });
                }
                if (string.IsNullOrEmpty(p_customer.Address))
                {
                    return BadRequest(new { Result = "Your address should not be empty!" });
                }
                if (string.IsNullOrEmpty(p_customer.PhoneNumber) || p_customer.PhoneNumber.All(Char.IsDigit))
                {
                    return BadRequest(new { Result = "Your phone number should not be empty!" });
                }
                //Add data to the Userdata table
                User _registerUser = new User();
                _registerUser.Username = p_customer.UserName;
                _registerUser.Password = p_customer.Password;

                _storeBL.RegisterUser(_registerUser);

                //Add data to the Customer table
                Customer _registerCustomer = new Customer();
                _registerCustomer.Name = p_customer.Name;
                _registerCustomer.Address = p_customer.Address;
                _registerCustomer.Phone = p_customer.PhoneNumber;
                _registerCustomer.Username = p_customer.UserName;

                _storeBL.AddCustomer(_registerCustomer);
                Log.Information("Added new user success " + _registerCustomer);


                return Created("Successfully added", p_customer);
            }
            catch (System.Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        //=====================================================================================================================================


        // [HttpPost("PlaceOrder")] // 5. Place Orders to a location
        // public IActionResult PlaceOrder([FromBody] ShoppingCart p_cart)
        // {

        // ShoppingCart _cart = new ShoppingCart();
        // _cart.CustomerID = p_cart.CustomerID;
        // _cart.StoreFrontID = p_cart.StoreFrontID;
        // _cart.ProductID = p_cart.ProductID;
        // _cart.Quantity = p_cart.Quantity;

        // _storeBL.PlaceOrder(_cart);

        // }

        //=====================================================================================================================================

        [HttpGet("GetCustomerOrderByID/{p_custid}")] // 6. View Order History of Customer
        public IActionResult GetCustomerOrderByID(int p_custid)
        {
            try
            {
                return Ok(_storeBL.GetCustomerOrderByID(p_custid)); //why it did not initially work on swagger is bc parameters were not added
            }
            catch (System.Exception)
            {

                return NotFound();
            }
        }

        //=====================================================================================================================================

        // [HttpGet("GetStoreOrderHistoryByID/{p_storeid}")] // 6. View Order History of Customer
        // public IActionResult GetStoreOrderHistoryByID(int p_storeid)
        // {
        //     try
        //     {
        //         return Ok(_storeBL.GetStoreOrderHistoryByID(p_storeid)); //why it did not initially work on swagger is bc parameters were not added
        //     }
        //     catch (System.Exception)
        //     {

        //         return NotFound();
        //     }
        // }

        //=====================================================================================================================================



        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Customer/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }


        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
