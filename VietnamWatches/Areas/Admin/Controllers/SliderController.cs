using MyClass.DAO;
using MyClass.Models;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ThietBiDienTu.Areas.Admin.Controllers
{
    public class SliderController : BaseController
    {
        SliderDAO sliderDAO = new SliderDAO();
        LinkDAO linkDAO = new LinkDAO();
        // GET: Admin/Supplier
        public ActionResult Index()
        {
            return View(sliderDAO.getList("Index"));
        }

        // GET: Admin/Supplier/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // GET: Admin/Supplier/Create
        public ActionResult Create()
        {
            ViewBag.ListOrder = new SelectList(sliderDAO.getList("Index"), "Orders", "Name", 0);
            return View();
        }

        // POST: Admin/Supplier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Slider slider)
        {
            if (ModelState.IsValid)
            {
                //Xử lý thêm thông tin
                if (slider.Orders == null)
                {
                    slider.Orders = 1;
                }
                else
                {
                    slider.Orders += 1;
                }

                //upload file img
                var fileImg = Request.Files["Img"];
                if (fileImg.ContentLength != 0)//Có chọn file
                {
                    string[] FileExtensions = new string[] { ".jpeg", ".jpg", ".png", ".gif", ".jepg" };
                    if (FileExtensions.Contains(fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."))))
                    {
                        string imgName = slider.Link + fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."));
                        //thực hiện lưu file
                        slider.Img = imgName;
                        string pathDir = "~/Public/images/sliders/";
                        string pathImg = Path.Combine(Server.MapPath(pathDir), imgName);
                        //thực hiện upload
                        fileImg.SaveAs(pathImg);
                        //thực hiện lưu file
                        slider.Img = imgName;
                    }
                }
                //end upload file img
                slider.CreatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                slider.UpdatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                slider.UpdatedAt = DateTime.Now;
                slider.CreatedAt = DateTime.Now;
                slider.Position = "SlideShow";
                if (sliderDAO.Insert(slider) == 1)
                {
                    Link link = new Link();
                    link.Slug = slider.Name;
                    link.TableId = slider.Id;
                    link.TypeLink = "slider";
                    linkDAO.Insert(link);
                }
                TempData["message"] = new XMessage("success", "Thêm thành công!");
                return RedirectToAction("Index");
            }
            ViewBag.ListOrder = new SelectList(sliderDAO.getList("Index"), "Orders", "Name", 0);
            return View(slider);
        }

        // GET: Admin/Supplier/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.ListOrder = new SelectList(sliderDAO.getList("Index"), "Orders", "Name", 0);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListOrder = new SelectList(sliderDAO.getList("Index"), "Orders", "Name", 0);
            return View(slider);
        }

        // POST: Admin/Supplier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Slider slider)
        {
            if (ModelState.IsValid)
            {
                if (slider.Orders == null)
                {
                    slider.Orders = 1;
                }
                else
                {
                    slider.Orders += 1;
                }
                //upload file img
                var fileImg = Request.Files["Img"];
                if (fileImg.ContentLength != 0)//Có chọn file
                {
                    string[] FileExtensions = new string[] { ".jpeg", ".jpg", ".png", ".gif", ".jepg" };
                    if (FileExtensions.Contains(fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."))))
                    {
                        string imgName = slider.Link + fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."));
                        //thực hiện lưu file
                        slider.Img = imgName;
                        string pathDir = "~/Public/images/slider/";
                        string pathImg = Path.Combine(Server.MapPath(pathDir), imgName);
                        //xoá file
                        if (slider.Img != null)
                        {
                            string delPath = Path.Combine(Server.MapPath(pathDir), imgName);
                            System.IO.File.Delete(delPath);
                        }
                        //thực hiện upload
                        fileImg.SaveAs(pathImg);
                    }
                }
                //end upload file img
                slider.UpdatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                slider.UpdatedAt = DateTime.Now;
                slider.Position = "Slideshow";
                //string dh = supplier.Slug;
                if (sliderDAO.Update(slider) == 1)
                {
                    Link link = linkDAO.getRow(slider.Id, "slider");
                    link.Slug = slider.Name;
                    linkDAO.Update(link);
                }
                TempData["message"] = new XMessage("success", "Cập nhật thành công!");
                return RedirectToAction("Index");
            }
            ViewBag.ListOrder = new SelectList(sliderDAO.getList("Index"), "Orders", "Name", 0);
            return View(slider);
        }

        // GET: Admin/Supplier/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Admin/Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slider slider = sliderDAO.getRow(id);
            //Xoá hình ảnh
            string pathDir = "~/Public/images/slider/";
            if (slider.Img != null)
            {
                string delPath = Path.Combine(Server.MapPath(pathDir), slider.Img);
                System.IO.File.Delete(delPath);
            }
            //---------------
            Link link = linkDAO.getRow(slider.Id, "slider");
            if (sliderDAO.Delete(slider) == 1)
            {
                linkDAO.Delete(link);
            }
            TempData["message"] = new XMessage("success", "Xoá thành công!");
            return RedirectToAction("Trash", "Slider");
        }

        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại!");
                return RedirectToAction("Index", "Slider");
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại!");
                return RedirectToAction("Index", "Slider");
            }
            if (slider.Status == 1)
            {
                slider.Status = 2;
            }
            else
            {
                slider.Status = 1;
            }
            slider.UpdatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            slider.UpdatedAt = DateTime.Now;
            sliderDAO.Update(slider);
            //TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công!");
            //return RedirectToAction("Index", "Slider");
            return Json(slider.Status);
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại!");
                return RedirectToAction("Index", "Slider");
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại!");
                return RedirectToAction("Index", "Slider");
            }

            slider.Status = 0; //Chuyen vao thung rac
            slider.UpdatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            slider.UpdatedAt = DateTime.Now;
            sliderDAO.Update(slider);
            TempData["message"] = new XMessage("success", "Chuyển vào thùng rác thành công!");
            return RedirectToAction("Index", "Slider");
        }
        public ActionResult Trash(int? id)
        {
            return View(sliderDAO.getList("Trash"));
        }
        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại!");
                return RedirectToAction("Trash", "Slider");
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại!");
                return RedirectToAction("Trash", "Slider");
            }

            slider.Status = 2; //Khôi phục trạng thái
            slider.UpdatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            slider.UpdatedAt = DateTime.Now;
            sliderDAO.Update(slider);
            TempData["message"] = new XMessage("success", "Khôi phục thành công!");
            return RedirectToAction("Trash", "Slider");
        }
    }
}
