using ModelEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEF.DAO
{
    public class CategoryDAO
    {
        private NguyenHuuTuanContext db;

        public CategoryDAO()
        {
            db = new NguyenHuuTuanContext();
        }

        public List<Category> listAll()
        {
            return db.Category.ToList();
        }

        public Category Find(int? id)
        {
            return db.Category.SingleOrDefault(x => x.ID == id);
        }

        public Category Update(Category model)
        {
            var category = Find(model.ID);
            category.Name = model.Name;
            category.Description = model.Description;
            db.SaveChanges();
            return Find(model.ID);
        }
    }
}
