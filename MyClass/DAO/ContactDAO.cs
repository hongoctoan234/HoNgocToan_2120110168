using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class ContactDAO
    {
        private TBDTDBContext db = new TBDTDBContext();
        //Trả về danh sách các mẫu tin
        public List<Contact> getList(string status = "All")
        {
            List<Contact> list = null;
            switch (status)
            {
                case "Index":
                    {
                        //Lấy ra những mẫu tin có status!=0
                        list = db.Contacts.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        //Lấy ra những mẫu tin có status==0
                        list = db.Contacts.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Contacts.ToList();
                        break;
                    }
            }
            return list;

        }
        //Trả vê 1 mẫu tin
        public Contact getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Contacts.Find(id);
            }
        }
        public Contact getRow(string name = null)
        {
            if (name == null)
            {
                return null;
            }
            else
            {
                return db.Contacts.Where(m => m.FullName == name).FirstOrDefault();
            }
        }
        //Thêm mẫu tin
        public int Insert(Contact row)
        {
            db.Contacts.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin
        public int Update(Contact row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xoá mẫu tin
        public int Delete(Contact row)
        {
            db.Contacts.Remove(row);
            return db.SaveChanges();
        }
    }
}
