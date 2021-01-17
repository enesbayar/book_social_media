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

        [HttpPost("Read")]
        public UserOperationResult readListUpdate([FromBody] CompositeObject compositeObject)
        {
            User user = compositeObject.user;
            Book book = compositeObject.book;
            return userOperation.readListUpdate(user, book);
        }
        [HttpPost("UserInfo")]
        public User userInfo([FromBody] LoginRequest loginRequest)
        {
            return userOperation.userInfo(loginRequest);
        } 

    }
}
