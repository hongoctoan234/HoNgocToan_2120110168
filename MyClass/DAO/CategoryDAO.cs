using MyClass.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MyClass.DAO
{
    public class CategoryDAO
    {
        private TBDTDBContext db = new TBDTDBContext();
        //Trả về danh sách các mẫu tin
        public List<Category> getListByParentId(int parentid = 0)
        {
            return db.Categorys.Where(m => m.ParentId == parentid && m.Status == 1).OrderBy(m => m.Orders).ToList();
        }
        //Trả về danh sách các mẫu tin
        public List<ProductInfo> getListCatPrd()
        {
            List<ProductInfo> list = null;
            list = db.Products
                            .Join(
                db.Categorys,
                p => p.CatId,
                c => c.Id,
                (p, c) => new ProductInfo
                {
                    Id = p.Id,
                    Name = p.Name,
                    CatId = p.CatId,
                    CatName = c.Name,
                    Slug = p.Slug,
                    SupplierId = p.SupplierId,
                    Images = p.Images,
                    Detail = p.Detail,
                    Number = p.Number,
                    Price = p.Price,
                    PriceSale = p.PriceSale,
                    MetaDesc = p.MetaDesc,
                    MetaKey = p.MetaKey,
                    CreatedBy = p.CreatedBy,
                    CreatedAt = p.CreatedAt,
                    UpdatedBy = p.UpdatedBy,
                    UpdatedAt = p.UpdatedAt,
                    SaleStart = p.SaleStart,
                    SaleEnd = p.SaleEnd,
                    Status = p.Status
                }
                ).Where(m => m.Status != 0)
                 .OrderByDescending(m => m.CreatedAt).ToList();
            return list;

        }
        //Trả về danh sách các mẫu tin
        public List<CategoryInfor> getList(string status = "All")
        {
            List<CategoryInfor> list = null;
            switch (status)
            {
                case "Index":
                    {
                        ////Lấy ra những mẫu tin có status!=0
                        //list = db.Categorys.Where(m => m.Status != 0).ToList();
                        //break;

                        //Lấy ra những mẫu tin có status!=0
                        list = db.Categorys
                            .Join(
                db.Users,
                p => p.UpdatedBy,
                c => c.Id,
                (p, c) => new CategoryInfor
                {
                    Id = p.Id,
                    Name = p.Name,
                    NameCreate = c.FullName,
                    Slug = p.Slug,
                    ParentId = p.ParentId,
                    Orders = p.Orders,
                    MetaDesc = p.MetaDesc,
                    MetaKey = p.MetaKey,
                    CreatedBy = p.CreatedBy,
                    CreatedAt = p.CreatedAt,
                    UpdatedBy = p.UpdatedBy,
                    UpdatedAt = p.UpdatedAt,
                    Status = p.Status
                }
                ).Where(m => m.Status != 0)
                 .OrderByDescending(m => m.CreatedAt).ToList();
                        break;
                    }
                case "Trash":
                    {

                        //Lấy ra những mẫu tin có status!=0
                        list = db.Categorys
                            .Join(
                db.Users,
                p => p.UpdatedBy,
                c => c.Id,
                (p, c) => new CategoryInfor
                {
                    Id = p.Id,
                    Name = p.Name,
                    NameCreate = c.FullName,
                    Slug = p.Slug,
                    ParentId = p.ParentId,
                    Orders = p.Orders,
                    MetaDesc = p.MetaDesc,
                    MetaKey = p.MetaKey,
                    CreatedBy = p.CreatedBy,
                    CreatedAt = p.CreatedAt,
                    UpdatedBy = p.UpdatedBy,
                    UpdatedAt = p.UpdatedAt,
                    Status = p.Status
                }
                ).Where(m => m.Status != 0)
                 .OrderByDescending(m => m.CreatedAt).ToList();
                        break;
                    }
                default:
                    {

                        //Lấy ra những mẫu tin có status!=0
                        list = db.Categorys
                            .Join(
                db.Users,
                p => p.UpdatedBy,
                c => c.Id,
                (p, c) => new CategoryInfor
                {
                    Id = p.Id,
                    Name = p.Name,
                    NameCreate = c.FullName,
                    Slug = p.Slug,
                    ParentId = p.ParentId,
                    Orders = p.Orders,
                    MetaDesc = p.MetaDesc,
                    MetaKey = p.MetaKey,
                    CreatedBy = p.CreatedBy,
                    CreatedAt = p.CreatedAt,
                    UpdatedBy = p.UpdatedBy,
                    UpdatedAt = p.UpdatedAt,
                    Status = p.Status
                }
                ).Where(m => m.Status != 0)
                 .OrderByDescending(m => m.CreatedAt).ToList();
                        break;
                    }
            }
            return list;

        }
        //Trả vê 1 mẫu tin
        public Category getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Categorys.Find(id);
            }
        }
        //Trả vê 1 mẫu tin
        public Category getRow(string slug)
        {
            if (slug == null)
            {
                return null;
            }
            else
            {
                return db.Categorys.Where(m => m.Slug == slug && m.Status == 1).FirstOrDefault();
            }
        }
        //Thêm mẫu tin
        public int Insert(Category row)
        {
            db.Categorys.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin
        public int Update(Category row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xoá mẫu tin
        public int Delete(Category row)
        {
            db.Categorys.Remove(row);
            return db.SaveChanges();
        }
    }
}
