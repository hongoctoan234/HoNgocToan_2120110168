using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;
using System.IO;
using ClosedXML.Excel;


namespace ThietBiDienTu.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        private TBDTDBContext db = new TBDTDBContext();

        OrderDAO orderDAO = new OrderDAO();
        OrderDetailDAO orderDetailDAO  = new OrderDetailDAO();
        // GET: Admin/Product 
        public ActionResult Index()
        {
            //List<OrderInfor> orderInfors = orderDAO.getListJoin("Index");
            List<Order> orderInfors = orderDAO.getList("Index");
            return View(orderInfors);
        }

        // GET: Admin/Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListChiTiet = orderDetailDAO.getList(id);
            return View(order);
        }

        // GET: Admin/Product/Create
        public ActionResult Create()
        {
             return View();
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
             return View(order);
        }

        // GET: Admin/Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
              
            }
            TempData["message"] = new XMessage("danger", "Cập nhật thành công!");
            return View(order);
        }

        // GET: Admin/Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = orderDAO.getRow(id);
            orderDAO.Delete(order);
            return RedirectToAction("Index");
        }

        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã sản phẩm không tồn tại!");
                return RedirectToAction("Index", "Order");
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại!");
                return RedirectToAction("Index", "Order");
            }
            if (order.Status == 1)
            {
                order.Status = 2;
            }
            else
            {
                order.Status = 1;
            }
            order.UpdatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            order.UpdatedAt = DateTime.Now;
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công!");
            return RedirectToAction("Index", "Order");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã sản phẩm không tồn tại!");
                return RedirectToAction("Index", "Order");
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại!");
                return RedirectToAction("Index", "Order");
            }

            order.Status = 0; //Chuyen vao thung rac
            order.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            order.UpdatedAt = DateTime.Now;
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Chuyển vào thùng rác thành công!");
            return RedirectToAction("Index", "Order");
        }
        public ActionResult Trash(int? id)
        {
            return View(orderDAO.getList("Trash"));
        }
        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã sản phẩm không tồn tại!");
                return RedirectToAction("Trash", "Order");
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại!");
                return RedirectToAction("Trash", "Order");
            }

            order.Status = 2; //Khôi phục trạng thái
            order.UpdatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            order.UpdatedAt = DateTime.Now;
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Khôi phục thành công!");
            return RedirectToAction("Trash", "Order");
        }
        public ActionResult Huy(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã sản phẩm không tồn tại!");
                return RedirectToAction("Index", "Order");
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại!");
                return RedirectToAction("Index", "Order");
            }
            if ((order.Status == 1)||(order.Status==2))
            {
                order.Status = 0;

                order.UpdatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                order.UpdatedAt = DateTime.Now;
            }
            else
            {
                if (order.Status == 3)
                {
                    TempData["message"] = new XMessage("danger", "Đơn hàng đang vận chuyển, không thể huỷ!");
                }
                else if (order.Status == 4)
                {
                    TempData["message"] = new XMessage("danger", "Đơn hàng đã giao thành công, không thể huỷ!");
                }
                return RedirectToAction("Index", "Order");
            }
            orderDAO.Update(order);
            TempData["message"] = new XMessage("info", "Đã huỷ đơn thành công!");
            return RedirectToAction("Index", "Order");
        }
        //[HttpPost]
        //public FileResult Export()
        //{
        //    OrderInfor entities = new OrderInfor();
        //    DataTable dt = new DataTable("Grid");
        //    dt.Columns.AddRange(new DataColumn[4] { new DataColumn("CustomerId"),
        //                                    new DataColumn("ContactName"),
        //                                    new DataColumn("City"),
        //                                    new DataColumn("Country") });

        //    OrderDetail orderDetail = new OrderDetail();
        //    orderDetail = orderDetailDAO.getRow(entities.OrderId);
        //    var orderDetailId = orderDetail.Se(x => x.CatId).Distinct();

        //    var listCat = orderDetailDAO.getList();
        //    List<int> repCat = new List<int>();
        //    var catId = listCat.Select(x => x.OrderId).Distinct();

        //    foreach (var customer in catId)
        //    {
        //        dt.Rows.Add(customer., customer.ContactName, customer.City, customer.Country);
        //    }

        //    using (XLWorkbook wb = new XLWorkbook())
        //    {
        //        wb.Worksheets.Add(dt);
        //        using (MemoryStream stream = new MemoryStream())
        //        {
        //            wb.SaveAs(stream);
        //            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
        //        }
        //    }
        //}
    }
}
