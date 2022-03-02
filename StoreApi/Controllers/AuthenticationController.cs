using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreModel;
using BL;
using System.Data.SqlClient;
using StoreApi.DataTransferObject;

namespace StoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IStoreBL _storeBL;

        public AuthenticationController(IStoreBL p_storeBL)
        {
            _storeBL = p_storeBL;
        }

        //=====================================================================================================================================

        [HttpPost("Register")]// 1. add a new customer
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
                if (string.IsNullOrEmpty(p_customer.PhoneNumber) || !p_customer.PhoneNumber.All(Char.IsDigit))
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

        //=====================================================================================================================================

    }
}
