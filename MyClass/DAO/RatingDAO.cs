using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class RatingDAO
    {
        private TBDTDBContext db = new TBDTDBContext();
        //
        //Trả về danh sách các mẫu tin
        public List<Rating> getListRatingByPrdId(int prdId)
        {
            return db.Ratings.Where(m => m.ProductId == prdId).OrderByDescending(m => m.CreatedAt).Take(5).ToList();
        }
        //Trả về danh sách các mẫu tin
        //public List<Rating> getList(string status = "All")
        //{
        //    List<Rating> list = null;
        //    switch (status)
        //    {
        //        case "Index":
        //            {
        //                //Lấy ra những mẫu tin có status!=0
        //                list = db.Ratings.Where(m => m.Status != 0).ToList();
        //                break;
        //            }
        //        case "Trash":
        //            {
        //                //Lấy ra những mẫu tin có status==0
        //                list = db.Ratings.Where(m => m.Status == 0).ToList();
        //                break;
        //            }
        //        default:
        //            {
        //                list = db.Ratings.ToList();
        //                break;
        //            }
        //    }
        //    return list;

        //}
        //Trả vê 1 mẫu tin
        public Rating getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Ratings.Find(id);
            }
        }


        //
        //Thêm mẫu tin
        public int Insert(Rating row)
        {
            db.Ratings.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin
        public int Update(Rating row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xoá mẫu tin
        public int Delete(Rating row)
        {
            db.Ratings.Remove(row);
            return db.SaveChanges();

        }
    }
}
