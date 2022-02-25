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
        /*
        [HttpGet] data annotation basically tells the complier that the method will be an action inside of a controller
        specifically this will handle a GET from the client and send a http request.
        */
        [HttpPost("New Customer Registration")]
        public IActionResult Register([FromBody] CustomerRegisterForm p_customer)
        {
            try
            {
                //Check all the fills are coorect
                if (string.IsNullOrWhiteSpace(p_customer.UserName))
                {
                    return BadRequest(new { Result = "Your username should not be empty or whitespace!" });
                }
                if (string.IsNullOrWhiteSpace(p_customer.Password) || p_customer.Password.Length < 6)
                {
                    return BadRequest(new { Result = "Your password should not be empty or whitespace!" });
                }
                if (string.IsNullOrWhiteSpace(p_customer.Name))
                {
                    return BadRequest(new { Result = "Your name should not be empty or whitespace!" });
                }
                if (string.IsNullOrEmpty(p_customer.Address))
                {
                    return BadRequest(new { Result = "Your address should not be empty!" });
                }
                if (string.IsNullOrEmpty(p_customer.PhoneNumber) || p_customer.PhoneNumber.All(Char.IsDigit))
                {
                    return BadRequest(new { Result = "Your phone number should not be empty !" });
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
                //set up logger



                //TODO
                //_storeBL.AddCustomer(_registerUser);

                return Created("Successfully added", p_customer);
            }
            catch (System.Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPost("PlaceOrder")]

        // GET: api/Store
        [HttpGet("GetAll")]//"GetAll" is the end point (shows up in swagger next to GET)
        public IActionResult GetAllCustomers() //action is a method inside of a controller
        {
            try
            {
                return Ok(_storeBL.GetAllCustomers());
            }
            catch (SqlException)
            {
                //Api is responsible for sending the right resource and the right status code
                //in this case if it was unable to connect to the db it will give a 404 status code

                return NotFound(); //It can be broke but if 0k method is used it will still return a 200 status code.
            }

            return Ok(_storeBL.GetAllCustomers());//Ok method gives 200 status code. Developer's job to give status codes.
        }

        // GET: api/Store/5
        [HttpGet("GetCustomerOrderByID/{custid}")]
        public IActionResult GetCustomerOrderByID(int custid)
        {
            try
            {
                return Ok(_storeBL.GetCustomerOrderByID(custid)); //why it did not initially work on swagger is bc parameters were not added
            }
            catch (System.Exception)
            {

                return NotFound();
            }
        }

        // POST: api/Store
        [HttpPost("Login")]
        public IActionResult Login([FromBody] User p_user)
        {
            try
            {
                if (_storeBL.Login(p_user))
                {
                    Log.Information("User logged in successfully" + p_user.Username);
                    return Ok(new { Results = "Login Successful." });
                }
                Log.Warning("Login Failure");
                return BadRequest(new { Result = "Login Failed!" });
            }
            catch (System.Exception e)
            {
                Log.Warning(e.Message);
                return StatusCode(500, e);
            }
        }

        // PUT: api/Store/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Store/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
