using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class SliderDAO
    {
        private TBDTDBContext db = new TBDTDBContext();
        //
        //Trả về danh sách các mẫu tin
        public List<Slider> getListByPostion(string pos)
        {
            return db.Sliders.Where(m => m.Position == pos && m.Status == 1).OrderBy(m => m.Orders).ToList();
        }
        //Trả về danh sách các mẫu tin
        public List<Slider> getList(string status = "All")
        {
            List<Slider> list = null;
            switch (status)
            {
                case "Index":
                    {
                        //Lấy ra những mẫu tin có status!=0
                        list = db.Sliders.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        //Lấy ra những mẫu tin có status==0
                        list = db.Sliders.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Sliders.ToList();
                        break;
                    }
            }
            return list;

        }
        //Trả vê 1 mẫu tin
        public Slider getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Sliders.Find(id);
            }
        }


        //
        //Thêm mẫu tin
        public int Insert(Slider row)
        {
            db.Sliders.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin
        public int Update(Slider row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xoá mẫu tin
        public int Delete(Slider row)
        {
            db.Sliders.Remove(row);
            return db.SaveChanges();

        }
    }
}
