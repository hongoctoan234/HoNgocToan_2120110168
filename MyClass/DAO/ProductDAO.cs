using MyClass.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class ProductDAO
    {
        private TBDTDBContext db = new TBDTDBContext();
        //
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
        public IPagedList<ProductInfo> getListByListCatID(List<int> listid, int pageSize, int pageNumber)
        {
            IPagedList<ProductInfo> list = db.Products
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
                )
                .Where(m => m.Status == 1 && listid.Contains(m.CatId))
                .OrderByDescending(m => m.CreatedAt)
                .ToPagedList(pageNumber, pageSize);
            return list;
        }
        public List<ProductInfo> getListByListCatIDHome(List<int> listid, int take)
        {
            List<ProductInfo> list = db.Products
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
                )
                .Where(m => m.Status == 1 && listid.Contains(m.CatId))
                .OrderByDescending(m => m.CreatedAt)
                .Take(take)
                .ToList();
            return list;
        }
        //---------------
        public List<ProductInfo> CheckDuplicatesNameProduct(string nameProduct)
        {
            List<ProductInfo> list = db.Products
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
                )
                .Where(m => nameProduct == m.CatName && nameProduct == m.Name)
                .ToList();
            return list;
        }
        //---------------
        public IPagedList<ProductInfo> getListByListSupID(List<int> listid, int pageSize, int pageNumber)
        {
            IPagedList<ProductInfo> list = db.Products
                .Join(
                db.Suppliers,
                p => p.SupplierId,
                c => c.Id,
                (p, c) => new ProductInfo
                {
                    Id = p.Id,
                    Name = p.Name,
                    CatId = p.CatId,
                    SupplierName = c.Name,
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
                )
                .Where(m => m.Status == 1 && listid.Contains(m.SupplierId))
                .OrderByDescending(m => m.CreatedAt)
                .ToPagedList(pageNumber, pageSize);
            return list;
        }
        //---------------
        public List<ProductInfo> getListByCatIdPrds(int? catId, int? notId)
        {
            List<ProductInfo> list = null;
            if (notId == null)
            {
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
                 )
                 .Where(m => m.Status == 1 && m.CatId == catId).Take(3)
                 .OrderByDescending(m => m.CreatedAt)
                 .ToList();
            }
            else
            {
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
                    )
                    .Where(m => m.Status == 1 && m.CatId == catId && m.Id != notId).Take(3)
                    .OrderByDescending(m => m.CreatedAt)
                    .ToList();
            }
            return list;
        }
        //Trả về danh sách các mẫu tin
        //---------------
        public IPagedList<ProductInfo> getList(int pageSize, int pageNumber)
        {
            IPagedList<ProductInfo> list = db.Products
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
                )
                .Where(m => m.Status == 1)
                .OrderByDescending(m => m.CreatedAt)
                .ToPagedList(pageNumber, pageSize);
            return list;
        }
        //Trả về danh sách các mẫu tin
        public List<ProductInfo> getList(string status = "All")
        {
            List<ProductInfo> list = null;
            switch (status)
            {
                case "Index":
                    {
                        //Lấy ra những mẫu tin có status!=0
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
                 .OrderByDescending(m => m.CreatedAt).ToList().Join(
                db.Suppliers,
                t => t.SupplierId,
                c => c.Id,
                (t, c) => new ProductInfo
                {
                    Id = t.Id,
                    Name = t.Name,
                    CatId = t.CatId,
                    CatName = t.CatName,
                    SupplierName = c.Name,
                    Slug = t.Slug,
                    SupplierId = t.SupplierId,
                    Images = t.Images,
                    Detail = t.Detail,
                    Number = t.Number,
                    Price = t.Price,
                    PriceSale = t.PriceSale,
                    MetaDesc = t.MetaDesc,
                    MetaKey = t.MetaKey,
                    CreatedBy = t.CreatedBy,
                    CreatedAt = t.CreatedAt,
                    UpdatedBy = t.UpdatedBy,
                    UpdatedAt = t.UpdatedAt,
                    SaleStart = t.SaleStart,
                    SaleEnd = t.SaleEnd,
                    Status = t.Status
                }
                ).Where(m => m.Status != 0)
                 .OrderByDescending(m => m.CreatedAt).ToList();
                        break;
                    }
                case "Trash":
                    {
                        //Lấy ra những mẫu tin có status!=0
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
                 .OrderByDescending(m => m.CreatedAt).ToList().Join(
                db.Suppliers,
                t => t.SupplierId,
                c => c.Id,
                (t, c) => new ProductInfo
                {
                    Id = t.Id,
                    Name = t.Name,
                    CatId = t.CatId,
                    CatName = t.CatName,
                    SupplierName = c.Name,
                    Slug = t.Slug,
                    SupplierId = t.SupplierId,
                    Images = t.Images,
                    Detail = t.Detail,
                    Number = t.Number,
                    Price = t.Price,
                    PriceSale = t.PriceSale,
                    MetaDesc = t.MetaDesc,
                    MetaKey = t.MetaKey,
                    CreatedBy = t.CreatedBy,
                    CreatedAt = t.CreatedAt,
                    UpdatedBy = t.UpdatedBy,
                    UpdatedAt = t.UpdatedAt,
                    SaleStart = t.SaleStart,
                    SaleEnd = t.SaleEnd,
                    Status = t.Status
                }
                ).Where(m => m.Status != 0)
                 .OrderByDescending(m => m.CreatedAt).ToList();
                        break;
                    }
                default:
                    {
                        //Lấy ra những mẫu tin có status!=0
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
                 .OrderByDescending(m => m.CreatedAt).ToList().Join(
                db.Suppliers,
                t => t.SupplierId,
                c => c.Id,
                (t, c) => new ProductInfo
                {
                    Id = t.Id,
                    Name = t.Name,
                    CatId = t.CatId,
                    CatName = t.CatName,
                    SupplierName = c.Name,
                    Slug = t.Slug,
                    SupplierId = t.SupplierId,
                    Images = t.Images,
                    Detail = t.Detail,
                    Number = t.Number,
                    Price = t.Price,
                    PriceSale = t.PriceSale,
                    MetaDesc = t.MetaDesc,
                    MetaKey = t.MetaKey,
                    CreatedBy = t.CreatedBy,
                    CreatedAt = t.CreatedAt,
                    UpdatedBy = t.UpdatedBy,
                    UpdatedAt = t.UpdatedAt,
                    SaleStart = t.SaleStart,
                    SaleEnd = t.SaleEnd,
                    Status = t.Status
                }
                ).Where(m => m.Status != 0)
                 .OrderByDescending(m => m.CreatedAt).ToList();
                        break;
                    }
            }
            return list;

        }

        public List<ProductInfo> getListChart()
        {
            List<ProductInfo> list = null;
                        //Lấy ra những mẫu tin có status!=0
                        return list = db.Products
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
                ).Where(m => m.Status != 0 && m.Number != 0)
                 .OrderBy(m => m.Number).ToList().Join(
                db.Suppliers,
                t => t.SupplierId,
                c => c.Id,
                (t, c) => new ProductInfo
                {
                    Id = t.Id,
                    Name = t.Name,
                    CatId = t.CatId,
                    CatName = t.CatName,
                    SupplierName = c.Name,
                    Slug = t.Slug,
                    SupplierId = t.SupplierId,
                    Images = t.Images,
                    Detail = t.Detail,
                    Number = t.Number,
                    Price = t.Price,
                    PriceSale = t.PriceSale,
                    MetaDesc = t.MetaDesc,
                    MetaKey = t.MetaKey,
                    CreatedBy = t.CreatedBy,
                    CreatedAt = t.CreatedAt,
                    UpdatedBy = t.UpdatedBy,
                    UpdatedAt = t.UpdatedAt,
                    SaleStart = t.SaleStart,
                    SaleEnd = t.SaleEnd,
                    Status = t.Status
                }
                ).Where(m => m.Status != 0 && m.Number!=0)
                 .OrderBy(m => m.Number).Take(10).ToList();
          
        }
        public List<Product> getListSearch(string seachString)
        {
            return db.Products.Where(m => m.Name.ToLower().Contains(seachString.ToLower())).OrderBy(m => m.CreatedAt).ToList();
        }

        public int getCountPrd()
        {
            return db.Products.Where(m => m.Status != 0).Count();
        }
        //Trả vê 1 mẫu tin
        public Product getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Products.Find(id);
            }
        }
        //Trả vê 1 mẫu tin
        public Product getRow(string slug)
        {
            if (slug == null)
            {
                return null;
            }
            else
            {
                return db.Products.Where(m => m.Slug == slug && m.Status == 1).FirstOrDefault();
            }
        }

        //
        //Thêm mẫu tin
        public int Insert(Product row)
        {
            db.Products.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin
        public int Update(Product row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xoá mẫu tin
        public int Delete(Product row)
        {
            db.Products.Remove(row);
            return db.SaveChanges();

        }
    }
}
