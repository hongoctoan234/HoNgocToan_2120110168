using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class ProductOptionDAO
    {
        private TBDTDBContext db = new TBDTDBContext();
        //Trả vê 1 mẫu tin
        public ProductOption getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.ProductOptions.Find(id);
            }
        }
        //Trả vê 1 mẫu tin
        public ProductOption getRow(int idPrd)
        {
            return db.ProductOptions.Where(m => m.IdProduct == idPrd).FirstOrDefault();
        }

        //
        //Thêm mẫu tin
        public int Insert(ProductOption row)
        {
            db.ProductOptions.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin
        public int Update(ProductOption row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xoá mẫu tin
        public int Delete(ProductOption row)
        {
            db.ProductOptions.Remove(row);
            return db.SaveChanges();
        }
    }
}
