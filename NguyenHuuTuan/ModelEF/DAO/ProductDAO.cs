using ModelEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEF.DAO
{
    public class ProductDAO
    {
        private NguyenHuuTuanContext db;

        public ProductDAO()
        {
            db = new NguyenHuuTuanContext();
        }

        public IEnumerable<Product> ListWhereAll(string name, int? category)
        {
            IQueryable<Product> model = db.Product;
            if (!string.IsNullOrEmpty(name))
            {
                model = model.Where(x => x.Name.Contains(name));
            }
            if (category != null && category != 0)
            {
                model = model.Where(x => x.ID_Category == category);
            }
            return model.Where(x => x.Status != false).OrderBy(x => x.Quantity).OrderByDescending(x => x.UnitCost).ToList();
        }

        public Product Find(int? id)
        {
            return db.Product.SingleOrDefault(x => x.ID == id);
        }

        public Product Create(Product model)
        {
            db.Product.Add(model);
            db.SaveChanges();
            var product = db.Product.ToList().LastOrDefault();
            return product;
        }

        public int GetNewID()
        {
            var productLast = db.Product.ToList().LastOrDefault();
            int id = productLast is null ? 1 : productLast.ID + 1;
            return id;
        }

        public Product Update(Product model)
        {
            var product = Find(model.ID);
            product.Name = model.Name;
            product.Quantity = model.Quantity;
            product.UnitCost = model.UnitCost;
            product.Image = model.Image;
            product.Description = model.Description;
            product.ID_Category = model.ID_Category;
            db.SaveChanges();
            return Find(product.ID);
        }

        public void Delete(int id)
        {
            Product product = Find(id);
            product.Status = false;
            db.SaveChanges();
        }
    }
}
