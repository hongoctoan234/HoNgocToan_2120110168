using MyClass.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MyClass.DAO
{
    public class DetailPrdDAO
    {
        private TBDTDBContext db = new TBDTDBContext();

        //
        //Trả về danh sách các mẫu tin
        public List<DetailPrd> getList(string status = "All")
        {
            List<DetailPrd> list = null;
            switch (status)
            {
                case "Index":
                    {
                        //Lấy ra những mẫu tin có status!=0
                        list = db.DetailPrds.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        //Lấy ra những mẫu tin có status==0
                        list = db.DetailPrds.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.DetailPrds.ToList();
                        break;
                    }
            }
            return list;

        }
        //Trả vê 1 mẫu tin
        public DetailPrd getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.DetailPrds.Find(id);
            }
        }


        //
        //Thêm mẫu tin
        public int Insert(DetailPrd row)
        {
            db.DetailPrds.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin
        public int Update(DetailPrd row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xoá mẫu tin
        public int Delete(DetailPrd row)
        {
            db.DetailPrds.Remove(row);
            return db.SaveChanges();

        }
    }
}
