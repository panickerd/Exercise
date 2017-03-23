using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exercise1.Models
{
    public class CategoryModel
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public object Id { get; set; }
        [Required]
        public string Name { get; set; }
    }

    public class CategoryActions : IOperation<CategoryModel>
    {
        private DataAccess _da;
        public CategoryActions(DataAccess da)
        {
            _da = da;
        }
        public CategoryModel Create(CategoryModel t)
        {
            _da._db.GetCollection<CategoryModel>("Category").Save(t);
            return t;
        }

        public CategoryModel GetItem(string id)
        {
            var res = Query.EQ("_id", id);
            return _da._db.GetCollection<CategoryModel>("Category").FindOne(res);
        }

        public IEnumerable<CategoryModel> GetItems()
        {
            return _da._db.GetCollection<CategoryModel>("Category").FindAll();
        }

        public void Remove(string id)
        {
            var subcrec = Query.EQ("_id", id);
            if (_da._db.GetCollection<SubCategoryModel>("SubCategory").Find(subcrec).Size() == 0)
            {
                var res = Query.EQ("_id", id);
                var operation = _da._db.GetCollection<CategoryModel>("Category").Remove(res);
            }
        }

        public void Update(string id, CategoryModel t)
        {
            var res = Query.EQ("_id", id);
            var operation = Update<CategoryModel>.Replace(t);
            _da._db.GetCollection<CategoryModel>("Category").Update(res, operation);
        }
    }
}