using ModelEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class UserDAO
    {
        private NguyenHuuTuanContext db;

        public UserDAO()
        {
            db = new NguyenHuuTuanContext();
        }

        public bool login(string username, string password)
        {
            var result = db.UserAccount.SingleOrDefault(x => x.UserName.Contains(username) && x.Password.Contains(password) && 
                x.Status != false);
            if (result == null)
            {
                return false;
            }
            return true;
        }


        //public List<Users> List()
        //{
        //    return db.Users.OrderByDescending(x=>x.id).ToList();
        //}

        public IEnumerable<UserAccount> ListWhereAll(string keyword)
        {
            IQueryable<UserAccount> model = db.UserAccount;
            if (!string.IsNullOrEmpty(keyword))
                model = model.Where(x => x.Name.Contains(keyword) || x.UserName.Contains(keyword));
            return model.Where(x=>x.Status != false).OrderByDescending(x => x.ID).ToList();
        }

        public void Create(UserAccount user)
        {
            db.UserAccount.Add(user);
            db.SaveChanges();
        }

        public UserAccount Update(UserAccount model)
        {
            var user = Find(model.ID);
            user.Name = model.Name;
            user.Password = model.Password;
            db.SaveChanges();
            return Find(model.ID);
        }

        public void Delete(int id)
        {
            UserAccount user = Find(id);
            user.Status = false;
            db.SaveChanges();
        }

        public UserAccount Find(int? id)
        {
            var result = db.UserAccount.SingleOrDefault(x=>x.ID == id);
            return result;
        }

        public UserAccount CheckUserName(string username)
        {
            var result = db.UserAccount.Where(x => x.UserName.Contains(username)).SingleOrDefault();
            return result;
        }
    }
}
