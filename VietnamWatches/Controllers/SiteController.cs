using MyClass.DAO;
using MyClass.Models;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ThietBiDienTu.Controllers
{
    public class SiteController : Controller
    {
        //TBDTDBContext db = new TBDTDBContext();
        //int somau = db.Products.Count();///
        //ViewBag.Somau = somau;
        ProductDAO productDAO = new ProductDAO();
        PostDAO postDAO = new PostDAO();
        LinkDAO linkDAO = new LinkDAO();
        CategoryDAO categoryDAO = new CategoryDAO();
        SupplierDAO supplierDAO = new SupplierDAO();
        TopicDAO topicDAO = new TopicDAO();
        // GET: Site
        public ActionResult Index(string slug = null, int? page = null)
        {
            TBDTDBContext db = new TBDTDBContext();
            int somau = db.Products.Count();
            ViewBag.Somau = somau;
            if (slug == null)
            {
                //trang chủ
                return this.Home();
            }
            else
            {
                //tìm slug có trong bảng Link
                Link link = linkDAO.getRow(slug);
                if (link != null)
                {
                    //Slug có trong bảng Link
                    string typeLink = link.TypeLink;
                    switch (typeLink)
                    {
                        case "category":
                            {
                                return this.ProductCategory(slug, page);
                            }
                        case "topic":
                            {
                                return this.PostTopic(slug, page);
                            }
                        case "page":
                            {
                                return this.PostPage(slug);
                            }
                        case "supplier":
                            {
                                return this.ProductSupplier(slug, page);
                            }
                        default:
                            {
                                return this.Error404(slug);
                            }
                    }
                }
                else
                {
                    //Slug không có trong bảng Link
                    //Slug có trong bản Product không?
                    Product product = productDAO.getRow(slug);
                    if (product != null)
                    {
                        return this.ProductDetail(product);
                    }
                    else
                    {
                        Post post = postDAO.getRowType(slug, "Post");
                        if (post != null)
                        {
                            return this.PostDetail(post);
                        }
                        else
                        {
                            return this.Error404(slug);
                        }
                    }

                    //Slug có trong bản Post có Post-style=Post không?
                }

            }
        }
        //===================================
        public ActionResult Home()
        {
            List<Category> list = categoryDAO.getListByParentId(0);
            return View("Home", list);
        }
        //===================================
        public ActionResult Product(int? page)
        {
            int pageNumber = page ?? 1; //Trang thứ bao nhiêu
            int pageSize = 9; //Số mẫu tin được hiển thị trong 1 trang

            IPagedList<ProductInfo> list = productDAO.getList(pageSize, pageNumber);
            return View("Product", list);
        }
        public ActionResult TatCaNhaCungCap(int? page)
        {
            int pageNumber = page ?? 1; //Trang thứ bao nhiêu
            int pageSize = 9; //Số mẫu tin được hiển thị trong 1 trang

            IPagedList<ProductInfo> list = productDAO.getList(pageSize, pageNumber);
            return View("TatCaNhaCungCap", list);
        }
        public ActionResult HomeProduct(int id)
        {
            Category category = categoryDAO.getRow(id);
            ViewBag.Category = category;
            //Danh mục loại theo 3 cấp
            List<int> listcatid = new List<int>();
            listcatid.Add(id);//cấp thứ 1
            List<Category> listcategory2 = categoryDAO.getListByParentId(id);
            if (listcatid.Count() != 0)
            {
                foreach (var cat2 in listcategory2)
                {
                    listcatid.Add(cat2.Id);//cấp thứ 2
                    List<Category> listcategory3 = categoryDAO.getListByParentId(cat2.Id);
                    if (listcategory3.Count() != 0)
                    {
                        foreach (var cat3 in listcategory3)
                        {
                            listcatid.Add(cat3.Id);//cấp thứ 3
                        }
                    }
                }
            }
            //
            List<ProductInfo> list = productDAO.getListByListCatIDHome(listcatid, 3);
            return View("HomeProduct", list);
        }
        public ActionResult ProductCategory(string slug, int? page)
        {

            int pageNumber = page ?? 1; //Trang thứ bao nhiêu
            int pageSize = 9; //Số mẫu tin được hiển thị trong 1 trang

            //Lấy category dựa vào slug
            Category category = categoryDAO.getRow(slug);
            ViewBag.Category = category;
            //Danh mục loại theo 3 cấp
            List<int> listcatid = new List<int>();
            listcatid.Add(category.Id);//cấp thứ 1
            List<Category> listcategory2 = categoryDAO.getListByParentId(category.Id);
            if (listcatid.Count() != 0)
            {
                foreach (var cat2 in listcategory2)
                {
                    listcatid.Add(cat2.Id);//cấp thứ 2
                    List<Category> listcategory3 = categoryDAO.getListByParentId(cat2.Id);
                    if (listcategory3.Count() != 0)
                    {
                        foreach (var cat3 in listcategory3)
                        {
                            listcatid.Add(cat3.Id);//cấp thứ 3
                        }
                    }
                }
            }
            //
            IPagedList<ProductInfo> list = productDAO.getListByListCatID(listcatid, pageSize, pageNumber);
            return View("ProductCategory", list);
        }
        public ActionResult ProductSupplier(string slug, int? page)
        {
            //Lấy category dựa vào slug
            Supplier supplier = supplierDAO.getRow(slug);
            ViewBag.Supplier = supplier;
            //Danh mục loại theo 3 cấp
            List<int> listcatid = new List<int>();
            listcatid.Add(supplier.Id);//cấp thứ 1
            List<Supplier> listcategory2 = supplierDAO.getListById(supplier.Id);
            if (listcatid.Count() != 0)
            {
                foreach (var cat2 in listcategory2)
                {
                    listcatid.Add(cat2.Id);//cấp thứ 2
                    List<Supplier> listcategory3 = supplierDAO.getListById(cat2.Id);
                    if (listcategory3.Count() != 0)
                    {
                        foreach (var cat3 in listcategory3)
                        {
                            listcatid.Add(cat3.Id);//cấp thứ 3
                        }
                    }
                }
            }
            //

            int pageNumber = page ?? 1; //Trang thứ bao nhiêu
            int pageSize = 9; //Số mẫu tin được hiển thị trong 1 trang
            IPagedList<ProductInfo> list = productDAO.getListByListSupID(listcatid, pageSize, pageNumber);
            return View("ProductSupplier", list);
        }
        public ActionResult ProductDetail(Product product)
        {
            //Sản phẩm liên quan
            ViewBag.ListOtherProduct = productDAO.getListByCatIdPrds(product.CatId, product.Id);
            return View("ProductDetail", product);
        }
        //===================================
        public ActionResult Post(int? page)
        {
            int pageNumber = page ?? 1; //Trang thứ bao nhiêu
            int pageSize = 6; //Số mẫu tin được hiển thị trong 1 trang

            IPagedList<PostInfo> list = postDAO.getList(pageSize, pageNumber);
            return View("Post", list);
        }
        public ActionResult PostTopic(string slug, int? page)
        {
            Topic topic = topicDAO.getRowSlug(slug);
            ViewBag.Topic = topic;
            List<PostInfo> list = postDAO.getListByTopicId(topic.Id, "Post", null);
            return View("PostTopic", list);
        }
        public ActionResult PostPage(string slug)
        {
            Post post = postDAO.getRowType(slug, "Page");
            return View("PostPage", post);
        }
        public ActionResult PostDetail(Post post)
        {
            //Bài viết liên quan
            ViewBag.ListOther = postDAO.getListByTopicId(post.TopId, "Post", post.Id);
            return View("PostDetail", post);
        }
        //===================================
        public ActionResult Error404(string slug)
        {
            return View("Error404");
        }
        public ActionResult HuyThanhCong(string slug)
        {
            return View("HuyThanhCong");
        }

    }
}