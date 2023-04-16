using MyClass.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MyClass.DAO
{
    public class TopicDAO
    {
        private TBDTDBContext db = new TBDTDBContext();
        //Trả về danh sách các mẫu tin
        public List<Topic> getList(string status = "All")
        {
            List<Topic> list = null;
            switch (status)
            {
                case "Index":
                    {
                        //Lấy ra những mẫu tin có status!=0
                        list = db.Topics.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        //Lấy ra những mẫu tin có status==0
                        list = db.Topics.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Topics.ToList();
                        break;
                    }
            }
            return list;

        }
        //Trả vê 1 mẫu tin
        public Topic getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Topics.Find(id);
            }
        }
        public Topic getRow(string name = null)
        {
            if (name == null)
            {
                return null;
            }
            else
            {
                return db.Topics.Where(m => m.Name == name).FirstOrDefault();
            }
        }
        public Topic getRowSlug(string slug)
        {
            return db.Topics.Where(m => m.Status == 1 && m.Slug == slug).FirstOrDefault();
        }
        //Thêm mẫu tin
        public int Insert(Topic row)
        {
            db.Topics.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin
        public int Update(Topic row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xoá mẫu tin
        public int Delete(Topic row)
        {
            db.Topics.Remove(row);
            return db.SaveChanges();
        }
    }
}
