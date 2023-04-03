using MyClass.DAO;
using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThietBiDienTu.Controllers
{
    public class RatingController : Controller
    {
        RatingDAO ratingDAO = new RatingDAO();
        UserDAO userDAO = new UserDAO();
        ProductDAO productDAO = new ProductDAO();
        // GET: Rating
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DoComment(int idProduct, string slugProduct, FormCollection field)
        {

            if (System.Web.HttpContext.Current.Session["CustomerId"].Equals(""))
            {
                string nameComment = field["username"];
                string emailComment = field["email"];
                string detailComment = field["comment"];

                Rating rating = new Rating();
                rating.UserName = nameComment;
                rating.Email = emailComment;
                rating.DetailRating = detailComment;
                rating.RatingStar = 5;
                rating.ProductId = idProduct;

                rating.CreatedAt = DateTime.Now;
                rating.UpdatedAt = DateTime.Now;
                ratingDAO.Insert(rating);
            }
            else
            {
                int idUser = Convert.ToInt32(Session["CustomerId"]);
                string detailComment = field["comment"];
                User user = userDAO.getRow(idUser);
                Rating rating = new Rating();
                rating.UserName = user.UserName;
                rating.Email = user.Email;
                rating.UserId = user.Id;
                rating.DetailRating = detailComment;
                rating.RatingStar = 5;
                rating.ProductId = idProduct;

                rating.CreatedAt = DateTime.Now;
                rating.UpdatedAt = DateTime.Now;
                ratingDAO.Insert(rating);
            }

            string slugReturn = "/" + slugProduct;

            return Redirect(slugReturn);
        }
    }
}