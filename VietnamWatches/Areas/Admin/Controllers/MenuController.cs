using MyClass.DAO;
using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace ThietBiDienTu.Areas.Admin.Controllers
{
    public class MenuController : Controller
    {
        CategoryDAO categoryDAO = new CategoryDAO();
        TopicDAO topicDAO = new TopicDAO();
        PostDAO postDAO = new PostDAO();
        SupplierDAO supplierDAO = new SupplierDAO();
        MenuDAO menuDAO = new MenuDAO();
        // GET: Admin/Menu
        public ActionResult Index()
        {
            ViewBag.ListCategory = categoryDAO.getList("Index");
            ViewBag.ListTopic = topicDAO.getList("Index");
            ViewBag.ListPage = postDAO.getList("Index", "Page");
            List<Menu> menu = menuDAO.getList("Index");
            return View("Index", menu);
        }
        // GET: Admin/Menu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            //Category
            if (!string.IsNullOrEmpty(form["ThemCategory"]))
            {
                if (!string.IsNullOrEmpty(form["nameCategory"]))
                {
                    var listitem = form["nameCategory"];
                    var listarr = listitem.Split(',');
                    foreach (var row in listarr) //Lấy ra Id cả Category
                    {
                        int id = int.Parse(row);
                        Category category = categoryDAO.getRow(id);
                        Menu menu = new Menu();
                        menu.Name = category.Name;
                        menu.Link = category.Slug;
                        menu.TableId = category.Id;
                        menu.Type = "category";
                        menu.Position = form["Position"];
                        menu.ParentId = 8; //0
                        menu.Orders = 0;
                        menu.Status = 2;
                        menu.CreatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                        menu.CreatedAt = DateTime.Now;
                        menu.UpdatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                        menu.UpdatedAt = DateTime.Now;
                        menuDAO.Insert(menu);
                    }
                    TempData["message"] = new XMessage("success", "Thêm menu thành công!");
                }
                else
                {
                    TempData["message"] = new XMessage("danger", "Chưa chọn danh mục sản phẩm!");
                }
            }
            //Topic
            if (!string.IsNullOrEmpty(form["ThemTopic"]))
            {
                if (!string.IsNullOrEmpty(form["nameTopic"]))
                {
                    var listitem = form["nameTopic"];
                    var listarr = listitem.Split(',');
                    foreach (var row in listarr) //Lấy ra Id cả Topic
                    {
                        int id = int.Parse(row);
                        Topic topic = topicDAO.getRow(id);
                        Menu menu = new Menu();
                        menu.Name = topic.Name;
                        menu.Link = topic.Slug;
                        menu.TableId = topic.Id;
                        menu.Type = "topic";
                        menu.Position = form["Position"];
                        menu.ParentId = 0;
                        menu.Orders = 0;
                        menu.Status = 2;
                        menu.CreatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                        menu.CreatedAt = DateTime.Now;
                        menu.UpdatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                        menu.UpdatedAt = DateTime.Now;
                        menuDAO.Insert(menu);
                    }
                    TempData["message"] = new XMessage("success", "Thêm menu thành công!");
                }
                else
                {
                    TempData["message"] = new XMessage("danger", "Chưa chọn chủ đề bài viết!");
                }
            }
            //Page
            if (!string.IsNullOrEmpty(form["ThemPage"]))
            {
                if (!string.IsNullOrEmpty(form["namePage"]))
                {
                    var listitem = form["namePage"];
                    var listarr = listitem.Split(',');
                    foreach (var row in listarr) //Lấy ra Id cả Page
                    {
                        int id = int.Parse(row);
                        Post post = postDAO.getRow(id);
                        Menu menu = new Menu();
                        menu.Name = post.Title;
                        menu.Link = post.Slug;
                        menu.TableId = post.Id;
                        menu.Type = "page";
                        menu.Position = form["Position"];
                        menu.ParentId = 0;
                        menu.Orders = 0;
                        menu.Status = 2;
                        menu.CreatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                        menu.CreatedAt = DateTime.Now;
                        menu.UpdatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                        menu.UpdatedAt = DateTime.Now;
                        menuDAO.Insert(menu);
                    }
                    TempData["message"] = new XMessage("success", "Thêm menu thành công!");
                }
                else
                {
                    TempData["message"] = new XMessage("danger", "Chưa chọn chủ đề bài viết!");
                }
            }

            //ThemCustom
            if (!string.IsNullOrEmpty(form["ThemCustom"]))
            {
                if (!string.IsNullOrEmpty(form["name"]) && !string.IsNullOrEmpty(form["link"]))
                {
                    Menu menu = new Menu();
                    menu.Name = form["name"];
                    menu.Link = form["link"];
                    menu.Type = "custom";
                    menu.Position = form["Position"];
                    menu.ParentId = 0;
                    menu.Orders = 0;
                    menu.Status = 2;
                    menu.CreatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                    menu.CreatedAt = DateTime.Now;
                    //menu.Updateby = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                    //menu.UpdateAt = DateTime.Now;
                    menuDAO.Insert(menu);
                    TempData["message"] = new XMessage("success", "Thêm menu thành công!");
                }
                else
                {
                    TempData["message"] = new XMessage("danger", "Chưa nhập đủ thông tin!");
                }
            }
            return RedirectToAction("Index", "Menu");
        }
        public ActionResult Edit(int? id)
        {
            ViewBag.ListMenu = new SelectList(menuDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(menuDAO.getList("Index"), "Orders", "Name", 0);
            Menu menu = menuDAO.getRow(id);
            return View("Edit", menu);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Menu menu)
        {
            if (ModelState.IsValid)
            {
                if (menu.ParentId == null)
                {
                    menu.ParentId = 0;
                }
                if (menu.Orders == null)
                {
                    menu.Orders = 1;
                }
                else
                {
                    menu.Orders += 1;
                }
                menu.UpdatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                menu.UpdatedAt = DateTime.Now;
                menuDAO.Update(menu);

                TempData["message"] = new XMessage("success", "Cập nhật thành công!");
                return RedirectToAction("Index");
            }
            ViewBag.ListMenu = new SelectList(menuDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(menuDAO.getList("Index"), "Orders", "Name", 0);

            return View(menu);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu menu = menuDAO.getRow(id);
            menuDAO.Delete(menu);
            TempData["message"] = new XMessage("success", "Xoá thành công!");
            return RedirectToAction("Trash", "Menu");
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại menu không tồn tại!");
                return RedirectToAction("Index", "Menu");
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại!");
                return RedirectToAction("Index", "Menu");
            }
            if (menu.Status == 1)
            {
                menu.Status = 2;
            }
            else
            {
                menu.Status = 1;
            }
            menu.UpdatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            menu.UpdatedAt = DateTime.Now;
            menuDAO.Update(menu);
            //TempData["message"] = new XMessage("success", "Thay đổi trạng thành công!");
            //return RedirectToAction("Index", "Menu");
            return Json(menu.Status);
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại menu không tồn tại!");
                return RedirectToAction("Index", "Menu");
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại!");
                return RedirectToAction("Index", "Menu");
            }

            menu.Status = 0; //Chuyen vao thung rac
            menu.UpdatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            menu.UpdatedAt = DateTime.Now;
            menuDAO.Update(menu);
            TempData["message"] = new XMessage("success", "Chuyển vào thùng rác thành công!");
            return RedirectToAction("Index", "Menu");
        }
        public ActionResult Trash(int? id)
        {
            return View(menuDAO.getList("Trash"));
        }
        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại Menu không tồn tại!");
                return RedirectToAction("Trash", "Menu");
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại!");
                return RedirectToAction("Trash", "Menu");
            }

            menu.Status = 2; //Khôi phục trạng thái
            menu.UpdatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            menu.UpdatedAt = DateTime.Now;
            menuDAO.Update(menu);
            TempData["message"] = new XMessage("success", "Khôi phục thành công!");
            return RedirectToAction("Trash", "Menu");
        }

    }
}
