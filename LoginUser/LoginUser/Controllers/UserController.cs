using LoginUser.Domain;
using LoginUser.Operation;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginUser.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("Cors")]
    [ApiController]

    public class UserController : ControllerBase
    {

        private readonly IUserOperation userOperation;

        public UserController(IUserOperation userOperation)
        {
            this.userOperation = userOperation;
        }

        [HttpPost("Create")]
        public UserOperationResult Create([FromBody] User user)
        {
            return userOperation.CreateUser(user);
        }

    }
}
