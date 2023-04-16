using MyClass.DAO;
using MyClass.Models;
using System;
using System.Web.Mvc;

namespace ThietBiDienTu.Controllers
{
    public class LienHeController : Controller
    {
        // GET: Contact
        ContactDAO contactDAO = new ContactDAO();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(FormCollection filed)
        {
            //Lấy thông tin
            String fullname = filed["fullname"];
            String email = filed["email"];
            String phone = filed["phone"];
            String title = filed["subject"];
            String detail = filed["noidung"];

            //Tạo một đối tượng thành viên
            Contact contact = new Contact();
            contact.FullName = fullname;
            contact.Email = email;
            contact.Phone = phone;
            contact.Title = title;
            contact.Detail = detail;
            contact.Status = 1;
            contact.CreatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            contact.CreatedAt = DateTime.Now;
            contact.UpdatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            contact.UpdatedAt = DateTime.Now;
            contact.ReplayDetail = "Chưa trả lời!";
            contactDAO.Insert(contact);
            return Redirect("~/lien-he");
        }

    }
}