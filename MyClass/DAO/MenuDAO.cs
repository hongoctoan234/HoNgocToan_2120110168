using MyClass.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MyClass.DAO
{
    public class MenuDAO
    {
        private TBDTDBContext db = new TBDTDBContext();
        public List<Menu> getListByParentId(string postion, int parentid = 0)
        {
            //Tra ve danh sach cho end users
            return db.Menus.Where(m => m.ParentId == parentid && m.Status == 1 && m.Position == postion)
                .OrderBy(m => m.Orders)
                .ToList();
        }
        //Trả về danh sách các mẫu tin
        public List<Menu> getList(string status = "All")
        {
            List<Menu> list = null;
            switch (status)
            {
                case "Index":
                    {
                        //Lấy ra những mẫu tin có status!=0
                        list = db.Menus.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        //Lấy ra những mẫu tin có status==0
                        list = db.Menus.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Menus.ToList();
                        break;
                    }
            }
            return list;

        }
        //Trả vê 1 mẫu tin
        public Menu getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Menus.Find(id);
            }
        }


        //
        //Thêm mẫu tin
        public int Insert(Menu row)
        {
            db.Menus.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin
        public int Update(Menu row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xoá mẫu tin
        public int Delete(Menu row)
        {
            db.Menus.Remove(row);
            return db.SaveChanges();

        }

    }
}
