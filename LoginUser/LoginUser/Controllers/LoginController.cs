using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginUser.Domain;
using LoginUser.Operation;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoginUser.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("Cors")]
    [ApiController]
    public class LoginController : ControllerBase{
        
        private readonly ILoginOperation loginOperation;

        public LoginController(ILoginOperation loginOperation)
        {
            this.loginOperation = loginOperation;
        }

        [HttpPost("Login")]
        public LoginResult Login([FromBody] LoginRequest loginRequest)
        {
            return loginOperation.Login(loginRequest);
        }
    }
}
