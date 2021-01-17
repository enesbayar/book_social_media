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
using Newtonsoft.Json.Linq;

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
        public BookResult incrementLikeCount([FromBody] CompositeObject compositeObject)
        {
            Book book = compositeObject.book;
            User user = compositeObject.user;
            return bookOperation.incrementLikeCount(book, user);
        }

        [HttpPost("NewComment")]
        public BookResult writeCommentToFile([FromBody] CompositeBookComment compositeBookComment)
        {
            Book book = compositeBookComment.book;
            string newComment = compositeBookComment.newComment;
            return bookOperation.WriteCommentToFile(book, newComment);
        }
        [HttpPost("ReadControl")]
        public BookResult readControl([FromBody] CompositeObject compositeObject)
        {
            Book book = compositeObject.book;
            User user = compositeObject.user;
            return bookOperation.readControl(user, book);
        }
    }
}
