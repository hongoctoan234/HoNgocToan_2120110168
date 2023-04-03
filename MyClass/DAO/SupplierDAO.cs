using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class SupplierDAO
    {
        private TBDTDBContext db = new TBDTDBContext();
        //
        public List<Supplier> getListViewHomePro()
        {
            return db.Suppliers.Where(m => m.Status == 1).OrderBy(m => m.Orders).ToList();
        }
        //Trả về danh sách các mẫu tin
        public List<Supplier> getList(string status = "All")
        {
            List<Supplier> list = null;
            switch (status)
            {
                case "Index":
                    {
                        //Lấy ra những mẫu tin có status!=0
                        list = db.Suppliers.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        //Lấy ra những mẫu tin có status==0
                        list = db.Suppliers.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Suppliers.ToList();
                        break;
                    }
            }
            return list;

        }
        //Trả vê 1 mẫu tin
        public Supplier getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Suppliers.Find(id);
            }
        }
        public List<Supplier> getListById(int id = 0)
        {
            return db.Suppliers.Where(m => m.Id == id && m.Status == 1).OrderBy(m => m.Orders).ToList();
        }
        //Trả vê 1 mẫu tin
        public Supplier getRow(string slug)
        {
            if (slug == null)
            {
                return null;
            }
            else
            {
                return db.Suppliers.Where(m => m.Slug == slug && m.Status == 1).FirstOrDefault();
            }
        }

        //
        //Thêm mẫu tin
        public int Insert(Supplier row)
        {
            db.Suppliers.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin
        public int Update(Supplier row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xoá mẫu tin
        public int Delete(Supplier row)
        {
            db.Suppliers.Remove(row);
            return db.SaveChanges();

        }

    }
}
