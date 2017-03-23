using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exercise1.Models
{
    public class ProductModel
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public object Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string CategoryId { get; set; }
    }

    public class ProductActions : IOperation<ProductModel>
    {
        private DataAccess _da;
        public ProductActions(DataAccess da)
        {
            _da = da;
        }
        public ProductModel Create(ProductModel t)
        {
            _da._db.GetCollection<ProductModel>("Product").Save(t);
            return t;
        }

        public ProductModel GetItem(string id)
        {
            var res = Query.EQ("_id", id);
            return _da._db.GetCollection<ProductModel>("Product").FindOne(res);
        }

        public IEnumerable<ProductModel> GetItems()
        {
            return _da._db.GetCollection<ProductModel>("Product").FindAll();
        }

        public void Remove(string id)
        {
            var res = Query.EQ("_id", id);
            _da._db.GetCollection<ProductModel>("Product").Remove(res);
        }

        public void Update(string id, ProductModel t)
        {
            var res = Query.EQ("_id", id);
            var operation = Update<ProductModel>.Replace(t);
            _da._db.GetCollection<ProductModel>("Product").Update(res, operation);
        }
    }
}
