using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;

namespace ThietBiDienTu.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        private TBDTDBContext db = new TBDTDBContext();

        ProductDAO productDAO = new ProductDAO();
        CategoryDAO categoryDAO = new CategoryDAO();
        SupplierDAO supplierDAO = new SupplierDAO();
        DetailPrdDAO detailPrdDAO = new DetailPrdDAO();
        ProductIMGDAO productIMGDAO = new ProductIMGDAO();
        ProductOptionDAO productOptionDAO = new ProductOptionDAO();
        // GET: Admin/Product 
        public ActionResult Index()
        {
            return View(productDAO.getList("Index"));
        }

        // GET: Admin/Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            ViewBag.ListSup = new SelectList(supplierDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListDetail = new SelectList(detailPrdDAO.getList("Index"), "Id", "Detail", 0);
            return View();
        }
        // POST: Admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Product product, HttpPostedFileBase[] ImgSecondary, FormCollection field)
        {
            if (ModelState.IsValid)
            {
                //Xử lý thêm thông tin
                product.Slug = XString.str_Slug(product.Name);
                //upload file img
                var fileImg = Request.Files["Img"];
                if (fileImg.ContentLength != 0)//Có chọn file
                {
                    string[] FileExtensions = new string[] { ".jpeg", ".jpg", ".png", ".gif", ".jepg" };
                    if (FileExtensions.Contains(fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."))))
                    {
                        string imgName = product.Slug + fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."));
                        product.Images = imgName;
                        string pathDir = "~/Public/images/products/";
                        string pathImg = Path.Combine(Server.MapPath(pathDir), imgName);
                        //thực hiện upload
                        fileImg.SaveAs(pathImg);
                        //thực hiện lưu file
                        product.Images = imgName;
                    }

                }
                product.CreatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                product.CreatedAt = DateTime.Now;
                product.UpdatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                product.UpdatedAt = DateTime.Now;

                string multiImages = "";
                if (ImgSecondary != null)
                {
                    int index = 1;
                    string fileName = "";
                    foreach (HttpPostedFileBase img in ImgSecondary)
                    {
                        if (img != null)
                        {
                            string extension = Path.GetExtension(img.FileName);
                            fileName = product.Slug + "-" + index + extension;
                            if (index != 1)
                            {
                                multiImages += "," + fileName;
                            }
                            else
                            {
                                multiImages += fileName;
                            }

                            string pathDir = "~/Public/images/products/";
                            img.SaveAs(Path.Combine(Server.MapPath(pathDir), fileName));
                            index++;
                        }
                    }
                }
                if (multiImages != "")
                {
                    product.ImageSecondary = multiImages;
                }

                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                var stringChars = new char[15];
                var random = new Random();
                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] += chars[random.Next(chars.Length)];
                }

                var finalString = new String(stringChars);
           
                product.MetaDesc = finalString;
                if (!field["calenderStart"].Equals(""))
                {
                    string calenderStart = field["calenderStart"];
                    DateTime oDateStart = DateTime.Parse(calenderStart);
                    product.SaleStart = oDateStart;
                }
                else
                {
                    product.SaleStart = DateTime.Now;
                }
                if (!field["calenderEnd"].Equals(""))
                {

                    string calenderEnd = field["calenderEnd"];
                    DateTime oDateEnd = DateTime.Parse(calenderEnd);
                    product.SaleEnd = oDateEnd;

                }
                else
                {
                    product.SaleEnd = DateTime.Now;
                }
                productDAO.Insert(product);
                TempData["message"] = new XMessage("success", "Thêm thành công!");
                return RedirectToAction("Index");
            }
            ViewBag.ListSup = new SelectList(supplierDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListDetail = new SelectList(detailPrdDAO.getList("Index"), "Id", "Detail", 0);
            return View(product);
        }

        // GET: Admin/Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListSup = new SelectList(supplierDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListDetail = new SelectList(detailPrdDAO.getList("Index"), "Id", "Detail", 0);
            return View(product);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Product product, HttpPostedFileBase[] ImgSecondary, FormCollection field)
        {
            ViewBag.ListSup = new SelectList(supplierDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListDetail = new SelectList(detailPrdDAO.getList("Index"), "Id", "Detail", 0);
            if (ModelState.IsValid)
            {
                //upload file img
                product.Slug = XString.str_Slug(product.Name);

                var fileImg = Request.Files["Img"];
                if (fileImg.ContentLength != 0)//Có chọn file
                {
                    string[] FileExtensions = new string[] { ".jpeg", ".jpg", ".png", ".gif", ".jepg" };
                    if (FileExtensions.Contains(fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."))))
                    {
                        string imgName = product.Slug + fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."));
                        product.Images = imgName;
                        string pathDir = "~/Public/images/products/";
                        string pathImg = Path.Combine(Server.MapPath(pathDir), imgName);
                        //Thực hiện xoá file
                        string DelPath = Path.Combine(Server.MapPath(pathDir), imgName);
                        if ((product.Images != null) && (System.IO.File.Exists(DelPath)))
                        {
                            System.IO.File.Delete(DelPath);//Xoá hình
                        }
                        //thực hiện upload
                        fileImg.SaveAs(pathImg);
                        //thực hiện lưu file
                        product.Images = imgName;
                    }
                }
                if (ImgSecondary != null)
                {
                    product.ImageSecondary = "";
                }

                string multiImages = "";
                if (ImgSecondary != null)
                {
                    int index = 1;
                    string fileName = "";
                    foreach (HttpPostedFileBase img in ImgSecondary)
                    {
                        if (img != null)
                        {
                            string extension = Path.GetExtension(img.FileName);
                            fileName = product.Slug + "-" + index + extension;
                            if (index != 1)
                            {
                                multiImages += "," + fileName;
                            }
                            else
                            {
                                multiImages += fileName;
                            }

                            string pathDir = "~/Public/images/products/";
                            img.SaveAs(Path.Combine(Server.MapPath(pathDir), fileName));
                            index++;
                        }
                    }
                }
                if (multiImages != "")
                {
                    product.ImageSecondary = multiImages;
                }
                //end upload file img
                product.UpdatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                product.UpdatedAt = DateTime.Now;

                if (!field["calenderStart"].Equals(""))
                {
                    string calenderStart = field["calenderStart"];
                    DateTime oDateStart = DateTime.Parse(calenderStart);
                    product.SaleStart = oDateStart;
                }
                if (!field["calenderEnd"].Equals(""))
                {

                    string calenderEnd = field["calenderEnd"];
                    DateTime oDateEnd = DateTime.Parse(calenderEnd);
                    product.SaleEnd = oDateEnd;

                }

                productDAO.Update(product);
                //if (productDAO.Update(product) == 1)
                //{
                //    ProductOption productOption = new ProductOption();
                //    productOption.Count = Request.Files.Count;

                //    if (productOptionDAO.Update(productOption) == 1)
                //    {
                //        var fileImgSecondary = Request.Files["ImgSecondary"];

                //        for (int i = 1; i < Request.Files.Count; i++)
                //        {
                //            if (Request.Files[i] != null && Request.Files[i].ContentLength > 0)
                //            {
                //                ProductIMG productIMG = new ProductIMG();
                //                productIMG.IdProduct = product.Id;
                //                productIMG.IdOption = product.Id;

                //                string[] FileExtensions = new string[] { ".jpeg", ".jpg", ".png", ".gif", ".jepg" };
                //                if (FileExtensions.Contains(fileImgSecondary.FileName.Substring(fileImgSecondary.FileName.LastIndexOf("."))))
                //                {
                //                    string imgNameSecondary = product.Slug + "-" + i + fileImgSecondary.FileName.Substring(fileImgSecondary.FileName.LastIndexOf("."));
                //                    productIMG.Images = imgNameSecondary;
                //                    string pathDir = "~/Public/images/products/";
                //                    string pathImg = Path.Combine(Server.MapPath(pathDir), imgNameSecondary);
                //                    //Thực hiện xoá file
                //                    string DelPath = Path.Combine(Server.MapPath(pathDir), imgNameSecondary);
                //                    if ((product.Images != null) && (System.IO.File.Exists(DelPath)))
                //                    {
                //                        System.IO.File.Delete(DelPath);//Xoá hình
                //                    }
                //                    //thực hiện upload
                //                    fileImgSecondary.SaveAs(pathImg);
                //                    //thực hiện lưu file
                //                    productIMG.Images = imgNameSecondary;
                //                }
                //                productIMGDAO.Update(productIMG);
                //            }
                //        }
                //    }

                //}
                return RedirectToAction("Index");
            }
            TempData["message"] = new XMessage("success", "Cập nhật thành công!");
            return View(product);
        }

        // GET: Admin/Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = productDAO.getRow(id);
            productDAO.Delete(product);
            return RedirectToAction("Index");
        }

        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã sản phẩm không tồn tại!");
                return RedirectToAction("Index", "Product");
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại!");
                return RedirectToAction("Index", "Product");
            }
            if (product.Status == 1)
            {
                product.Status = 2;
            }
            else
            {
                product.Status = 1;
            }
            product.UpdatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            product.UpdatedAt = DateTime.Now;
            productDAO.Update(product);
            //TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công!");
            //return RedirectToAction("Index", "Product");
            return Json(product.Status);

        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã sản phẩm không tồn tại!");
                return RedirectToAction("Index", "Product");
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại!");
                return RedirectToAction("Index", "Product");
            }

            product.Status = 0; //Chuyen vao thung rac
            product.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            product.UpdatedAt = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success", "Chuyển vào thùng rác thành công!");
            return RedirectToAction("Index", "Product");
        }
        public ActionResult Trash(int? id)
        {
            return View(productDAO.getList("Trash"));
        }
        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã sản phẩm không tồn tại!");
                return RedirectToAction("Trash", "Product");
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại!");
                return RedirectToAction("Trash", "Product");
            }

            product.Status = 2; //Khôi phục trạng thái
            product.UpdatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            product.UpdatedAt = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success", "Khôi phục thành công!");
            return RedirectToAction("Trash", "Product");
        }
        public String NameImgOrder(int? productId)
        {
            Product product = productDAO.getRow(productId);
            if (product == null)
            {
                return "";
            }
            else
            {
                return product.Images;
            }
        }
        public String NameProductOrder(int? productId)
        {
            Product product = productDAO.getRow(productId);
            if (product == null)
            {
                return "";
            }
            else
            {
                return product.Name;
            }
        }
    }
}
