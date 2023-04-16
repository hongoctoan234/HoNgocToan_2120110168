using MyClass.DAO;
using MyClass.Models;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ThietBiDienTu.Areas.Admin.Controllers
{
    public class SupplierController : BaseController
    {
        SupplierDAO supplierDAO = new SupplierDAO();
        LinkDAO linkDAO = new LinkDAO();

        // GET: Admin/Product 
        public ActionResult Index()
        {
            return View(supplierDAO.getList("Index"));
        }
        // GET: Admin/Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            ViewBag.ListOrder = new SelectList(supplierDAO.getList("Index"), "Id", "Name", 0);
            return View();
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                //Xử lý thêm thông tin
                supplier.Slug = XString.str_Slug(supplier.Name);
                supplier.CreatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                supplier.CreatedAt = DateTime.Now;

                //upload file img
                var fileImg = Request.Files["Img"];
                if (fileImg.ContentLength != 0)//Có chọn file
                {
                    string[] FileExtensions = new string[] { ".jpeg", ".jpg", ".png", ".gif", ".jepg" };
                    if (FileExtensions.Contains(fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."))))
                    {
                        string imgName = supplier.Slug + fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."));
                        supplier.Img = imgName;
                        string pathDir = "~/Public/images/suppliers/";
                        string pathImg = Path.Combine(Server.MapPath(pathDir), imgName);
                        //thực hiện upload
                        fileImg.SaveAs(pathImg);
                        //thực hiện lưu file
                        supplier.Img = imgName;
                    }
                }
                supplier.CreatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                supplier.CreatedAt = DateTime.Now;
                supplier.UpdatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                supplier.UpdatedAt = DateTime.Now;

                //end upload file img
                if (supplierDAO.Insert(supplier) == 1)
                {
                    Link link = new Link();
                    link.Slug = supplier.Slug;
                    link.TableId = supplier.Id;
                    link.TypeLink = "supplier";
                    linkDAO.Insert(link);
                }
                TempData["message"] = new XMessage("success", "Thêm thành công!");
                return RedirectToAction("Index");
            }
            ViewBag.ListOrder = new SelectList(supplierDAO.getList("Index"), "Id", "Name", 0);
            return View(supplier);
        }

        // GET: Admin/Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListOrder = new SelectList(supplierDAO.getList("Index"), "Id", "Name", 0);
            return View(supplier);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Supplier supplier)
        {

            if (ModelState.IsValid)
            {
                //upload file img
                supplier.Slug = XString.str_Slug(supplier.Name);

                var fileImg = Request.Files["Img"];
                if (fileImg.ContentLength != 0)//Có chọn file
                {
                    string[] FileExtensions = new string[] { ".jpeg", ".jpg", ".png", ".gif", ".jepg" };
                    if (FileExtensions.Contains(fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."))))
                    {
                        string imgName = supplier.Slug + fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."));
                        supplier.Img = imgName;
                        string pathDir = "~/Public/images/suppliers/";
                        string pathImg = Path.Combine(Server.MapPath(pathDir), imgName);
                        //Thực hiện xoá file
                        if (supplier.Img != null)
                        {
                            string DelPath = Path.Combine(Server.MapPath(pathDir), imgName);
                            System.IO.File.Delete(DelPath);//Xoá hình
                        }
                        //thực hiện upload
                        fileImg.SaveAs(pathImg);
                        //thực hiện lưu file
                        supplier.Img = imgName;
                    }
                }
                //end upload file img
                supplier.UpdatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                supplier.UpdatedAt = DateTime.Now;
                if (supplierDAO.Update(supplier) == 1)
                {
                    Link link = linkDAO.getRow(supplier.Id, "supplier");
                    link.Slug = supplier.Slug;
                    linkDAO.Update(link);
                    //Cap nhat menu
                }
                TempData["message"] = new XMessage("danger", "Cập nhật thành công!");
                return RedirectToAction("Index");
            }
            ViewBag.ListOrder = new SelectList(supplierDAO.getList("Index"), "Id", "Name", 0);
            return View(supplier);
        }

        // GET: Admin/Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supplier supplier = supplierDAO.getRow(id);
            Link link = linkDAO.getRow(supplier.Id, "supplier");
            if (supplierDAO.Delete(supplier) == 1)
            {
                linkDAO.Delete(link);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã nhà cung cấp không tồn tại!");
                return RedirectToAction("Index", "Supplier");
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại!");
                return RedirectToAction("Index", "Supplier");
            }
            if (supplier.Status == 1)
            {
                supplier.Status = 2;
            }
            else
            {
                supplier.Status = 1;
            }
            supplier.UpdatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            supplier.UpdatedAt = DateTime.Now;
            supplierDAO.Update(supplier);
            //TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công!");
            //return RedirectToAction("Index", "Supplier");
            return Json(supplier.Status);
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã nhà cung cấp không tồn tại!");
                return RedirectToAction("Index", "Supplier");
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại!");
                return RedirectToAction("Index", "Supplier");
            }

            supplier.Status = 0; //Chuyen vao thung rac
            supplier.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            supplier.UpdatedAt = DateTime.Now;
            supplierDAO.Update(supplier);
            TempData["message"] = new XMessage("success", "Chuyển vào thùng rác thành công!");
            return RedirectToAction("Index", "Supplier");
        }
        public ActionResult Trash(int? id)
        {
            return View(supplierDAO.getList("Trash"));
        }
        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã nhà cung cấp không tồn tại!");
                return RedirectToAction("Trash", "Supplier");
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại!");
                return RedirectToAction("Trash", "Supplier");
            }

            supplier.Status = 2; //Khôi phục trạng thái
            supplier.UpdatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            supplier.UpdatedAt = DateTime.Now;
            supplierDAO.Update(supplier);
            TempData["message"] = new XMessage("success", "Khôi phục thành công!");
            return RedirectToAction("Trash", "Supplier");
        }
    }
}
