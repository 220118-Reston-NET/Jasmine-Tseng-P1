using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BL;

namespace StoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IStoreBL _storeBL;

        public AdminController(IStoreBL p_storeBL)
        {
            _storeBL = p_storeBL;
        }

        [HttpPost("AssignManagerRoleToUser")]
        public IActionResult AssignManagerRoleToUser([FromQuery] string p_username)
        {
            try
            {
                //Check if the username is existing in the database
                if (_storeBL.GetAllCustomers().All(p => p.Username != p_username))
                {
                    return BadRequest("This username need to be a customer before requesting to open the store!");
                }
                //Check if the username is already store manager, if not will assign
                if (_storeBL.GetAllStores().Any(p => p.Username.Equals(p_username)))
                {
                    return BadRequest("This username is already a store manager");
                }
                _storeBL.AssignMangerRoleToUser(p_username);
                return Ok("Manager role assigned successfully!");
            }
            catch (System.Exception e)
            {
                return StatusCode(422, e);
            }
        }
    }
}
