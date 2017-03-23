using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise1.Models
{
    public class DataAccess
    {
        MongoClient _client;
        MongoServer _server;
        internal MongoDatabase _db;

        public DataAccess()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _server = _client.GetServer();
            _db = _server.GetDatabase("Exercise");
        }
    }

    public interface IOperation<T>
    {
        IEnumerable<T> GetItems();
        T GetItem(string id);
        T Create(T t);
        void Update(string id, T t);
        void Remove(string id);
    }
}
