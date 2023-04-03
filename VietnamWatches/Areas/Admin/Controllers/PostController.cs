using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;

namespace ThietBiDienTu.Areas.Admin.Controllers
{
    public class PostController : BaseController
    {
        PostDAO postDAO = new PostDAO();
        TopicDAO topicDAO = new TopicDAO();
        LinkDAO linkDAO = new LinkDAO();
        // GET: Admin/Supplier 
        public ActionResult Index()
        {
            return View(postDAO.getList("Index", "Post"));
        }

        // GET: Admin/Supplier/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Admin/Supplier/Create
        public ActionResult Create()
        {
            ViewBag.ListPost = new SelectList(topicDAO.getList("Index"), "Id", "Name", 0);
            return View();
        }

        // POST: Admin/Supplier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                //Xử lý thêm thông tin
                post.Slug = XString.str_Slug(post.Title);
                //upload file img
                var fileImg = Request.Files["Img"];
                if (fileImg.ContentLength != 0)//Có chọn file
                {
                    string[] FileExtensions = new string[] { ".jpeg", ".jpg", ".png", ".gif", ".jepg" };
                    if (FileExtensions.Contains(fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."))))
                    {
                        string imgName = post.Slug + fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."));
                        post.Img = imgName;
                        string pathDir = "~/Public/images/posts/";
                        string pathImg = Path.Combine(Server.MapPath(pathDir), imgName);
                        //thực hiện upload
                        fileImg.SaveAs(pathImg);
                        //thực hiện lưu file
                        post.Img = imgName;
                    }
                }
                //end upload file img
                post.CreatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                post.CreatedAt = DateTime.Now;
                post.UpdatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                post.UpdatedAt = DateTime.Now;
                post.Type = "Post";

                postDAO.Insert(post);
                TempData["message"] = new XMessage("success", "Thêm thành công!");
                return RedirectToAction("Index");
            }
            ViewBag.ListPost = new SelectList(topicDAO.getList("Index"), "Id", "Name", 0);
            return View(post);
        }

        // GET: Admin/Supplier/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListPost = new SelectList(topicDAO.getList("Index"), "Id", "Name", 0);
            return View(post);
        }

        // POST: Admin/Supplier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                //upload file img
                post.Slug = XString.str_Slug(post.Title);

                var fileImg = Request.Files["Img"];
                if (fileImg.ContentLength != 0)//Có chọn file
                {
                    string[] FileExtensions = new string[] { ".jpeg", ".jpg", ".png", ".gif", ".jepg" };
                    if (FileExtensions.Contains(fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."))))
                    {
                        string imgName = post.Slug + fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."));
                        post.Img = imgName;
                        string pathDir = "~/Public/images/posts/";
                        string pathImg = Path.Combine(Server.MapPath(pathDir), imgName);
                        //Thực hiện xoá file
                        string DelPath = Path.Combine(Server.MapPath(pathDir), imgName);
                        if ((post.Img != null) && (System.IO.File.Exists(DelPath)))
                        {
                            System.IO.File.Delete(DelPath);//Xoá hình
                        }
                        //thực hiện upload
                        fileImg.SaveAs(pathImg);
                        //thực hiện lưu file
                        post.Img = imgName;
                    }
                }
                //end upload file img
                post.UpdatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                post.UpdatedAt = DateTime.Now;
                postDAO.Update(post);
                return RedirectToAction("Index");
            }
            TempData["message"] = new XMessage("danger", "Cập nhật thành công!");
            return View(post);

        }

        // GET: Admin/Supplier/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Admin/Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = postDAO.getRow(id);
            //Xoá hình ảnh
            string pathDir = "~/Public/images/posts/";
            if (post.Img != null)
            {
                string delPath = Path.Combine(Server.MapPath(pathDir), post.Img);
                System.IO.File.Delete(delPath);
            }
            //---------------
            Link link = linkDAO.getRow(post.Id, "post");
            if (postDAO.Delete(post) == 1)
            {
                linkDAO.Delete(link);
            }
            TempData["message"] = new XMessage("success", "Xoá thành công!");
            return RedirectToAction("Trash", "Post");
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại!");
                return RedirectToAction("Index", "Post");
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại!");
                return RedirectToAction("Index", "Post");
            }
            if (post.Status == 1)
            {
                post.Status = 2;
            }
            else
            {
                post.Status = 1;
            }
            post.UpdatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            post.UpdatedAt = DateTime.Now;
            postDAO.Update(post);
            //TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công!");
            //return RedirectToAction("Index", "Product");
            return Json(post.Status);
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Post không tồn tại!");
                return RedirectToAction("Index", "Post");
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại!");
                return RedirectToAction("Index", "Post");
            }

            post.Status = 0; //Chuyen vao thung rac
            post.UpdatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            post.UpdatedAt = DateTime.Now;
            postDAO.Update(post);
            TempData["message"] = new XMessage("success", "Chuyển vào thùng rác thành công!");
            return RedirectToAction("Index", "Post");
        }
        public ActionResult Trash(int? id)
        {
            return View(postDAO.getListTrash("Post"));
        }
        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Post không tồn tại!");
                return RedirectToAction("Trash", "Post");
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại!");
                return RedirectToAction("Trash", "Post");
            }

            post.Status = 2; //Khôi phục trạng thái
            post.UpdatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            post.UpdatedAt = DateTime.Now;
            postDAO.Update(post);
            TempData["message"] = new XMessage("success", "Khôi phục thành công!");
            return RedirectToAction("Trash", "Post");
        }

    }
}
