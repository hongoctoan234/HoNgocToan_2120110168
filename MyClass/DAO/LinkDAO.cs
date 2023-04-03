using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class LinkDAO
    {
        private TBDTDBContext db = new TBDTDBContext();
        //Trả vê 1 mẫu tin
        public Link getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Links.Find(id);
            }
        }
        //Trả vê 1 mẫu tin
        public Link getRow(string slug)
        {
            if (slug == null)
            {
                return null;
            }
            else
            {
                return db.Links.Where(m => m.Slug == slug).FirstOrDefault();
            }
        }
        //Trả vê 1 mẫu tin
        public Link getRow(int tableid, string typelink)
        {
            return db.Links.Where(m => m.TableId == tableid && m.TypeLink == typelink).FirstOrDefault();
        }

        //
        //Thêm mẫu tin
        public int Insert(Link row)
        {
            db.Links.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin
        public int Update(Link row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xoá mẫu tin
        public int Delete(Link row)
        {
            db.Links.Remove(row);
            return db.SaveChanges();
        }

    }
}
