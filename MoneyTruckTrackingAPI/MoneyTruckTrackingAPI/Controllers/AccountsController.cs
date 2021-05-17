using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MoneyTruckTrackingAPI.Requests;
using MoneyTruckTrackingAPI.ServiceInterfaces;
using MoneyTruckTrackingAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyTruckTrackingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accService;

        public AccountsController(IAccountService accService)
        {
            _accService = accService;
        }
        

        // POST /api/accounts/login
        [HttpPost()]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest req)
        {
            if(req == null || string.IsNullOrEmpty(req.Username) || string.IsNullOrEmpty(req.Password))
            {
                return BadRequest("Missing Username or Password");
            }

            var res = await _accService.Login(req);
            if(res == null)
            {
                return BadRequest($"{req.Username} login unsuccessfully");
            }

            return Ok(res);
        }

        // POST /api/accounts/signup
        [HttpPost()]
        [Route("signup")]
        public async Task<IActionResult> Signup(SignupRequest req)
        {
            if (CheckNull(req))
            {
                return BadRequest("Can't create new Account");
            }

            if (string.IsNullOrEmpty(req.Admin))
            {
                req.Admin = "false";
            }

            var res = await _accService.Signup(req);

            if(res == null)
            {
                return BadRequest($"Error when create account {req.Username}");
            }
            return Ok(res);
        }

        public bool CheckNull(SignupRequest req)
        {
            if (req == null) return true;
            if (string.IsNullOrEmpty(req.Username)) return true;
            if (string.IsNullOrEmpty(req.Name)) return true;
            if (string.IsNullOrEmpty(req.Password)) return true;
            if (string.IsNullOrEmpty(req.PasswordSalt)) return true;
            if (string.IsNullOrEmpty(req.Email)) return true;
            if (string.IsNullOrEmpty(req.Phone)) return true;
            if (string.IsNullOrEmpty(req.Unit)) return true;
            return false;
        }
    }
}
