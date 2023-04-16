using MyClass.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ThietBiDienTu.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {

        OrderDAO orderDAO = new OrderDAO();
        OrderDetailDAO orderDetailDAO = new OrderDetailDAO();
        ProductDAO productDAO = new ProductDAO();
        UserDAO userDAO = new UserDAO();
        CategoryDAO categoryDAO = new CategoryDAO();
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            ViewBag.ListOrderJoin = orderDAO.getListJoin("Index");
            ViewBag.ListProductCount = productDAO.getCountPrd();
            ViewBag.ListCat = categoryDAO.getList("Index");
            ViewBag.ListUserCount = userDAO.getCountUser();

            return View();
        }
        public ActionResult Chart()
        {
            ViewBag.ListOrderJoin = orderDAO.getListJoin("Index");
            ViewBag.ListProduct = productDAO.getListChart();
            ViewBag.ListCat = categoryDAO.getList("Index");
            ViewBag.ListUserCount = userDAO.getCountUser();

            //chart Category
            var listCat = productDAO.getListCatPrd();
            List<int> repCat = new List<int>();
            var catId = listCat.Select(x => x.CatId).Distinct();
            foreach (var item in catId)
            {
                repCat.Add(listCat.Count(x => x.CatId == item));
            }
            var rep = repCat;
            var catname = listCat.Select(x => x.CatName).Distinct();
            ViewBag.AGES = catname;
            ViewBag.REP = repCat.ToList();

            //chart Doanh Thu
            var listOrder = orderDAO.getListJoin("Index");
            List<decimal> repOrder = new List<decimal>();
            var orderId = listOrder.Select(m => m.Id).Distinct();
            foreach (var item in orderId)
            {
                repOrder.Add(listOrder.Where(x => x.OrderId == item).Sum(i => i.Amount));
            }

            var listOrder1 = orderDAO.getListJoin("Index");
            List<DateTime> orderTime = new List<DateTime>();
            var orderTimeId = listOrder.Select(m => m.CreateDate).Distinct();
            //foreach (var item in orderTimeId)
            //{
            //    orderTime.Add(listOrder1.Where(x => x.CreateDate == item).Distinct());
            //}
            ViewBag.TimeOrder = listOrder.Select(m => m.CreateDate).Distinct();
            ViewBag.AmountOrder = repOrder.ToList();
            return View();
        }
    }
}