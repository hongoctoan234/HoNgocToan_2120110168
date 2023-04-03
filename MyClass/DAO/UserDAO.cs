using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class UserDAO
    {
        private TBDTDBContext db = new TBDTDBContext();
        //Trả về danh sách các mẫu tin
        public int getCountUser()
        {
            return db.Users.Where(m => m.Status != 0).Count();
        }
        public List<User> getList(string status = "All")
        {
            List<User> list = null;
            switch (status)
            {
                case "Index":
                    {
                        //Lấy ra những mẫu tin có status!=0
                        list = db.Users.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        //Lấy ra những mẫu tin có status==0
                        list = db.Users.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Users.ToList();
                        break;
                    }
            }
            return list;

        }
        public List<User> getList(int? id)
        {
            return db.Users.Where(m => m.Id == id).ToList();
        }
        public List<User> getList()
        {
            List<User> list = db.Users.ToList();
            return list;
        }

        //Trả vê 1 mẫu tin
        public User getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Users.Find(id);
            }
        }
        public User getRow(string name = null)
        {
            if (name == null)
            {
                return null;
            }
            else
            {
                return db.Users.Where(m => m.UserName == name).FirstOrDefault();
            }
        }
        public User getRowCustomer(string name, int role)
        {
            if (name == null)
            {
                return null;
            }
            else
            {
                return db.Users
                    .Where(m => m.Status == 1 && m.Access == role && (m.UserName == name || m.Email == name))
                    .FirstOrDefault();
            }
        }
        public User getRowUserNameAndEmail(string name, string email)
        {
            if (name == null || email == null)
            {
                return null;
            }
            else
            {
                return db.Users
                    .Where(m => m.Status == 1 && m.Access == 2 && (m.UserName == name || m.Email == email))
                    .FirstOrDefault();
            }
        }
        //Thê
        public User getRowAdmin(string name)
        {
            if (name == null)
            {
                return null;
            }
            else
            {
                return db.Users
                    .Where(m => m.Status == 1 && m.Access == 1 && (m.UserName == name || m.Email == name))
                    .FirstOrDefault();
            }
        }
        //Thêm mẫu tin
        public int Insert(User row)
        {
            db.Users.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin
        public int Update(User row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xoá mẫu tin
        public int Delete(User row)
        {
            db.Users.Remove(row);
            return db.SaveChanges();
        }

    }
}
