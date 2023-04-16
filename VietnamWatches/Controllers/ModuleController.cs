using MyClass.DAO;
using MyClass.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ThietBiDienTu.Controllers
{
    public class ModuleController : Controller
    {
        private MenuDAO menuDAO = new MenuDAO();
        private SliderDAO sliderDAO = new SliderDAO();
        private CategoryDAO categoryDAO = new CategoryDAO();
        private PostDAO postDAO = new PostDAO();
        private SupplierDAO supplierDAO = new SupplierDAO();
        private ProductIMGDAO productIMGDAO = new ProductIMGDAO();
        private RatingDAO ratingDAO = new RatingDAO();
        private ProductDAO productDAO = new ProductDAO();
        private ConfigDAO configDAO = new ConfigDAO();

        // GET: Module
        public ActionResult MainMenu()
        {
            List<Menu> list = menuDAO.getListByParentId("mainmenu", 0);
            //List<Menu> list = menuDAO.getListByParentId(0);
            return View("MainMenu", list);
        }
        public ActionResult MainMenuSub(int id)
        {
            Menu menu = menuDAO.getRow(id);
            List<Menu> list = menuDAO.getListByParentId("mainmenu", id);
            if (list.Count == 0)
            {
                //Không có cấp con
                return View("MainMenuSub1", menu);
            }
            else
            {
                //Có cấp con
                ViewBag.Menu = menu;
                return View("MainMenuSub2", list);
            }
        }
        //SlideShow
        public ActionResult SlideShow()
        {
            List<Slider> list = sliderDAO.getListByPostion("SlideShow");
            return View("SlideShow", list);
        }
        //SlideDetailPrd
        public ActionResult SlideDetailPrd()
        {
            return View("SlideDetailPrd");
        }
        //OurNews
        public ActionResult OurNews()
        {
            var type = "Post";
            List<PostInfo> list = postDAO.getListByTypePostInfo(type);
            return View("OurNews", list);
        }
        //ListCategory
        public ActionResult ListCategory()
        {
            List<Category> list = categoryDAO.getListByParentId(0);
            return View("ListCategory", list);
        }
        //ListCatProduct
        public ActionResult ListCatProduct()
        {
            List<Category> list = categoryDAO.getListByParentId(0);
            return View("ListCatProduct", list);
        }
        //ListPostProduct
        public ActionResult ListPostProduct()
        {
            var type = "Post";
            List<Post> list = postDAO.getListPostByType(type);
            return View("ListPostProduct", list);
        }
        //ListCategory
        public ActionResult ListSupProduct()
        {
            List<Supplier> list = supplierDAO.getListViewHomePro();
            return View("ListSupProduct", list);
        }
        //BreadCrumb
        public ActionResult BreadCrumb()
        {
            return View("BreadCrumb");
        }
        //BreadCrumb
        public ActionResult FillPrice()
        {
            return View("FillPrice");
        }
        public ActionResult Comments(int id)
        {
            List<Rating> list = ratingDAO.getListRatingByPrdId(id);
            Product product = productDAO.getRow(id);
            ViewBag.SlugPrd = product.Slug;
            ViewBag.IdPrd = product.Id;
            return View("Comments", list);
        }
        //PostLastNews
        public ActionResult PostLastNews()
        {
            return View("PostLastNews");
        }
        //PostLastNews
        public ActionResult MenuFooter()
        {
            List<Menu> list = menuDAO.getListByParentId("footermenu", 0);
            return View("MenuFooter", list);
        }
        public ActionResult ConfigFooter()
        {
            Config configAddress = configDAO.getRowByValue("Địa Chỉ");
            if (configAddress != null)
            {
                ViewBag.ConfigAddress = configAddress.Value;
            }
            else
            {
                ViewBag.ConfigAddress = "";
            }
            Config configPhone = configDAO.getRowByValue("Số Điện Thoại");
            if (configPhone != null)
            {
                ViewBag.ConfigPhone = configPhone.Value;
            }
            else
            {
                ViewBag.ConfigPhone = "";
            }
            Config configInfo = configDAO.getRowByValue("Giới Thiệu Chung");
            if (configInfo != null)
            {
                ViewBag.ConfigInfo = configInfo.Value;
            }
            else
            {
                ViewBag.ConfigInfo = "";
            }
            Config configEmail = configDAO.getRowByValue("Email");
            if (configEmail != null)
            {
                ViewBag.ConfigEmail = configEmail.Value;
            }
            else
            {
                ViewBag.ConfigEmail = "";
            }
            return View("ConfigFooter");
        }
    }
}