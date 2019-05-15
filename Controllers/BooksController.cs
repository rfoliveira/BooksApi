using System.Collections.Generic;
using System.Threading.Tasks;
using BooksApi.Models;
using BooksApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    public class BooksController: Controller
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(_bookService.Get());
        }

        [HttpGet("{id:length(24)}", Name="GetBook")]
        public IActionResult Get(string id)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            return new ObjectResult(book);
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            _bookService.Create(book);

            /*
                The CreatedAtRoute method returns a 201 response, 
                which is the standard response for an HTTP POST method that creates a new resource on the server. 
                CreatedAtRoute also adds a Location header to the response. 
                The Location header specifies the URI of the newly created to-do item.
             */
            return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Book bookIn)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookService.Update(id, bookIn);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound(new { IdInformado = id });
            }

            _bookService.Remove(book.Id);

            return NoContent();
        }

        #region Async methods
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var lista = await _bookService.GetAsync();
            return new ObjectResult(lista);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Book book)
        {
            await _bookService.CreateAsync(book);

            return CreatedAtRoute("GetBookAsync", new { id = book.Id.ToString() }, book);
        }

        [HttpGet("{id:length(24)}", Name="GetBookAsync")]
        public async Task<IActionResult> GetAsync(string id)
        {
            var book = await _bookService.GetAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return new ObjectResult(book);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateAsync(string id, Book bookIn)
        {
            var book = await _bookService.GetAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookService.Update(id, bookIn);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var book = await _bookService.GetAsync(id);

            if (book == null)
            {
                return NotFound(new { IdInformado = id });
            }

            _bookService.Remove(book.Id);

            return NoContent();
        }
        #endregion
    }
}