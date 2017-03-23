using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exercise1.Models
{
    public class SubCategoryModel
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public object Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string CategoryId { get; set; }
    }

    public class SubCategoryActions : IOperation<SubCategoryModel>
    {
        private DataAccess _da;
        public SubCategoryActions(DataAccess da)
        {
            _da = da;
        }
        public SubCategoryModel Create(SubCategoryModel t)
        {
            _da._db.GetCollection<SubCategoryModel>("SubCategory").Save(t);
            return t;
        }

        public SubCategoryModel GetItem(string id)
        {
            var res = Query.EQ("_id", id);
            return _da._db.GetCollection<SubCategoryModel>("SubCategory").FindOne(res);
        }

        public IEnumerable<SubCategoryModel> GetItems()
        {
            return _da._db.GetCollection<SubCategoryModel>("SubCategory").FindAll();
        }

        public void Remove(string id)
        {
            var subcrec = Query.EQ("_id", id);
            if (_da._db.GetCollection<ProductModel>("Product").Find(subcrec).Size() == 0)
            {
                var res = Query.EQ("_id", id);
                _da._db.GetCollection<SubCategoryModel>("Category").Remove(res);
            }
        }

        public void Update(string id, SubCategoryModel t)
        {
            var res = Query.EQ("_id", id);
            var operation = Update<SubCategoryModel>.Replace(t);
            _da._db.GetCollection<SubCategoryModel>("SubCategory").Update(res, operation);
        }
    }
}
