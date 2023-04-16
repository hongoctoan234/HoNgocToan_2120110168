using MyClass.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MyClass.DAO
{
    public class ConfigDAO
    {
        private TBDTDBContext db = new TBDTDBContext();
        //Trả về danh sách các mẫu tin
        public List<Config> getList(string status = "All")
        {
            List<Config> list = null;
            switch (status)
            {
                case "Index":
                    {
                        //Lấy ra những mẫu tin có status!=0
                        list = db.Configs.ToList();
                        break;
                    }
                case "Trash":
                    {
                        //Lấy ra những mẫu tin có status==0
                        list = db.Configs.ToList();
                        break;
                    }
                default:
                    {
                        list = db.Configs.ToList();
                        break;
                    }
            }
            return list;

        }
        public List<Config> getList(int? id)
        {
            return db.Configs.Where(m => m.Id == id).ToList();
        }
        public List<Config> getList()
        {
            List<Config> list = db.Configs.ToList();
            return list;
        }
        //Trả vê 1 mẫu tin
        public Config getRowByValue(string name)
        {

            return db.Configs.Where(m => m.Name == name).FirstOrDefault();
        }
        //Trả vê 1 mẫu tin
        public Config getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Configs.Find(id);
            }
        }

        //Thêm mẫu tin
        public int Insert(Config row)
        {
            db.Configs.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin
        public int Update(Config row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xoá mẫu tin
        public int Delete(Config row)
        {
            db.Configs.Remove(row);
            return db.SaveChanges();
        }
    }
}
