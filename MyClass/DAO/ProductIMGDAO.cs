using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class ProductIMGDAO
    {
        private TBDTDBContext db = new TBDTDBContext();
        public List<ProductIMG> getById(int id)
        {
            return db.ProductIMGs.Where(m => m.IdProduct == id).ToList();
        }
        //Trả về danh sách các mẫu tin
        public List<ProductOption_IMG> getListByOption(string optionName, int id)
        {
            List<ProductOption_IMG> list = db.ProductIMGs
                .Join(
                db.ProductOptions,
                p => p.IdOption,
                c => c.Id,
                (p, c) => new ProductOption_IMG
                {
                    Id = p.Id,
                    IdOption = c.Id,
                    IdProduct = p.IdProduct,
                    OptionName = c.OptionName,
                    Count = c.Count,
                    Images = p.Images
                }
                )
                .Where(m => m.OptionName == optionName && m.IdProduct==id)
                .ToList()
                .Join(
                db.Products,
                p => p.IdProduct,
                c => c.Id,
                (p, c) => new ProductOption_IMG
                {
                    ImageAvt=c.Images
                }
                )
                .Where(m => m.OptionName == optionName && m.IdProduct == id)
                .ToList();
            return list;
        }
        //Trả về danh sách các mẫu tin
        public List<ProductIMG> getList(string status = "All")
        {
            List<ProductIMG> list = null;
            switch (status)
            {
                case "Index":
                    {
                        //Lấy ra những mẫu tin có status!=0
                        break;
                    }
                case "Trash":
                    {
                        //Lấy ra những mẫu tin có status==0
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            return list;

        }
        public ProductIMG getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.ProductIMGs.Find(id);
            }
        }
        //Trả vê 1 mẫu tin
        public ProductIMG getRow(int idPrd)
        {
            return db.ProductIMGs.Where(m => m.IdProduct == idPrd).FirstOrDefault();
        }
        //Thêm mẫu tin
        public int Insert(ProductIMG row)
        {
            db.ProductIMGs.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin
        public int Update(ProductIMG row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xoá mẫu tin
        public int Delete(ProductIMG row)
        {
            db.ProductIMGs.Remove(row);
            return db.SaveChanges();
        }
    }
}
