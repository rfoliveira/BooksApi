using System.Collections.Generic;
using System.Threading.Tasks;
using BooksApi.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace BooksApi.Services
{
    public class PersonService
    {
        private readonly IMongoCollection<Person> _persons;

        public PersonService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("BookstoreDb"));
            var database = client.GetDatabase("BookstoreDb");
            _persons = database.GetCollection<Person>("Persons");
        }

        public async Task<IList<Person>> GetAll()
        {
            return await _persons.FindAsync(p => true).Result.ToListAsync();
        }
    }
}