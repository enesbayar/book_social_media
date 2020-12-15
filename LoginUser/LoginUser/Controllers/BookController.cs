using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using LoginUser.Operation;
using LoginUser.Domain;
using System.Collections;

namespace LoginUser.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("Cors")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookOperation bookOperation;

        public BookController(IBookOperation bookOperation)
        {
            this.bookOperation = bookOperation;
        }

        [HttpGet("Book")]
        public ArrayList getBook()
        {
            return bookOperation.ReadBook();
        }

        [HttpPost("CreateBook")]
        public BookResult createBook([FromBody] Book book)
        {
            return bookOperation.CreateBook(book);
        }

        [HttpPost("Increment")]
        public BookResult incrementLikeCount([FromBody] Book book)
        {
            return bookOperation.incrementLikeCount(book);
        }
    }
}
