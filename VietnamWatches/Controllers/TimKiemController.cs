using MyClass.DAO;
using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ThietBiDienTu.Controllers
{
    public class TimKiemController : Controller
    {
        ProductDAO productDAO = new ProductDAO();
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DoReSearch(string searchString)
        {
            Product product = productDAO.getRow(searchString);
            ViewBag.SearchString = searchString;
            List<Product> list = null;
            if (!String.IsNullOrEmpty(searchString))
            {
                list = productDAO.getListSearch(searchString);
            }
            return View("Index", list);
        }
    }
}