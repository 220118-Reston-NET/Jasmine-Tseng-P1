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
        public IActionResult AddCustomer(/*[FromBody]*/Customer p_customer)
        {
            try
            {
                return Created("Successfully added", _storeBL.AddCustomer(p_customer));
            }
            catch (System.Exception ex)
            {
                return Conflict(ex.Message);
            }
        }
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
//  List<Order> GetCustomerOrderByID(int p_custid);
        // POST: api/Store
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
