using BooksApi.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace BooksApi.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;

        public BookService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("BookstoreDb"));
            var database = client.GetDatabase("BookstoreDb");
            _books = database.GetCollection<Book>("Books");
        }

        public List<Book> Get()
        {
            return _books.Find(b => true).ToList();
        }

        public Book Get(string id)
        {
            return _books.Find<Book>(b => b.Id == id).FirstOrDefault();
        }

        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        public void Update(string id, Book bookIn)
        {
            _books.ReplaceOne(b => b.Id == id, bookIn);
        }

        public void Remove(Book bookIn)
        {
            _books.DeleteOne(b => b.Id == bookIn.Id);
        }

        public void Remove(string id)
        {
            _books.DeleteOne(b => b.Id == id);
        }

        #region Async methods
        public async Task<IEnumerable<Book>> GetAsync()
        {
            return await _books.FindAsync(b => true).Result.ToListAsync();
        }

        public async Task<Book> GetAsync(string id)
        {
            return await _books.FindAsync(b => b.Id == id).Result.FirstOrDefaultAsync();
        }

        public async Task<Book> CreateAsync(Book book)
        {
            await _books.InsertOneAsync(book);
            return book;
        }

        public async Task UpdateAsync(string id, Book bookIn)
        {
            await _books.ReplaceOneAsync(b => b.Id == id, bookIn);
        }

        public async Task RemoveAsync(string id)
        {
            await _books.DeleteOneAsync(b => b.Id == id);
        }
        #endregion
    }
}