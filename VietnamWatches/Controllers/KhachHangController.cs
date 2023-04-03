using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;
using System.IO;

namespace ThietBiDienTu.Controllers
{
    public class KhachHangController : Controller
    {
        UserDAO userDAO = new UserDAO();
        OrderDAO orderDAO = new OrderDAO();
        OrderDetailDAO orderdetailDAO = new OrderDetailDAO();
        ProductDAO productDAO = new ProductDAO();
        XCustomer xCus = new XCustomer();
        XCart xCart = new XCart();

        // GET: KhachHang
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public JsonResult DoLogin(string useridForm, string passwordForm)
        {
            string username = useridForm;
            string password = XString.ToMD5(passwordForm);
            User rowUser = userDAO.getRowCustomer(username, 2); //2 là Access của Customer
            string strError = "";
            if (rowUser == null)
            {
                strError = "Tên đăng nhập không tồn tại";
            }
            else
            {
                if (password.Equals(rowUser.Password))
                {
                    Session["UserCustomer"] = username;
                    Session["FullNameCustomer"] = rowUser.FullName;
                    Session["CustomerId"] = rowUser.Id;
                    return Json(new { Success = true, Message = "Đăng nhập thành công!!!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    strError = "Mật khẩu không đúng, vui lòng thử lại";
                }
            }
            ViewBag.Error = strError;
            return Json(new { Success = false, Message = strError }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DangKi()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DoRegister(FormCollection field)
        {
            string strError = "";
            User user = new User();
            //Lấy thông tin
            String username = field["username"];
            String password = XString.ToMD5(field["password"]);
            String confirmPassword = XString.ToMD5(field["confirmPassword"]);
            if (!password.Equals(confirmPassword))
            {
                strError = "Mật khẩu không trùng, vui lòng thử lại";
                ViewBag.Error = strError;
                return View("DangKi");
            }
            String email = field["email"];
            if (userDAO.getRowUserNameAndEmail(username, email) != null)
            {
                strError = "Tên đăng nhập hoặc email đã tồn tại, vui lòng thử lại";
                ViewBag.Error = strError;
                return View("DangKi");
            }
            String fullname = field["fullname"];
            String phone = field["phone"];
            String address = field["address"];
            int gender = 0;
            //if (!field["genderMale"].Equals(""))
            //{
            //    gender = 1;
            //}
            //else
            //{
            //    gender = 2;
            //}
            //Tạo một đối tượng thành viên
            user.UserName = username;
            user.Password = password;
            user.FullName = fullname;
            user.Email = email;
            user.Phone = phone;
            user.Address = address;
            user.Access = 2;
            user.Gender = gender;
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            user.Status = 1;

            string slug = XString.str_Slug(user.UserName);
            var fileImg = Request.Files["imageCustomer"];
            if (fileImg.ContentLength != 0)//Có chọn file
            {
                string[] FileExtensions = new string[] { ".jpeg", ".jpg", ".png", ".gif", ".jepg" };
                if (FileExtensions.Contains(fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."))))
                {
                    string imgName = slug + fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."));
                    //thực hiện lưu file
                    user.Images = imgName;
                    string pathDir = "~/Public/images/users/";
                    string pathImg = Path.Combine(Server.MapPath(pathDir), imgName);
                    //thực hiện upload
                    fileImg.SaveAs(pathImg);
                    //thực hiện lưu file
                    user.Images = imgName;
                }
            }
            userDAO.Insert(user);
            //User row_user = userDAO.getRow(username, "Customer");
            //Session["NameCustomer"] = row_user.FullName;
            //Session["UserCustomer"] = row_user.UserName;
            //Session["CustomerId"] = row_user.Id;
            return Redirect("~/login");
        }
        public ActionResult DangXuat()
        {
            Session["UserCustomer"] = "";
            Session["FullNameCustomer"] = "";
            Session["CustomerId"] = "";
            return Redirect("~/");

        }
        [HttpPost]
        public JsonResult ChangePass(string passwordNow, string passwordNew, string passwordNewConfirm)
        {
            int userid = int.Parse(Session["CustomerId"].ToString());//Mã người đăng nhập

            User user = userDAO.getRow(userid);

            string strError = "";
            if (!XString.ToMD5(passwordNow).Equals(user.Password))
            {
                strError = "Mật khẩu cũ không trùng khớp, vui lòng thử lại";
                ViewBag.Error = strError;
                return Json(new { Success = false, Message = strError }, JsonRequestBehavior.AllowGet);
            }
            if (!passwordNew.Equals(passwordNewConfirm))
            {
                strError = "Mật khẩu xác nhận không trùng khớp, vui lòng thử lại";
                ViewBag.Error = strError;
                return Json(new { Success = false, Message = strError }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                user.Password = XString.ToMD5(passwordNew);
                userDAO.Update(user);
                strError = "Thay đổi mật khẩu thành công";
                ViewBag.Error = strError;
                return Json(new { Success = true, Message = strError }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult DoChangeInfoCus(string fullNameChange, string emailChange, string phoneChange, string addressChange)
        {
            int userid = int.Parse(Session["CustomerId"].ToString());//Mã người đăng nhập

            User user = userDAO.getRow(userid);

            string strError = "";
            if ((fullNameChange.Equals("")) || (emailChange.Equals("")) || (phoneChange.Equals("")) || (addressChange.Equals("")))
            {
                strError = "Bạn đang để trống thông tin cần thiết!";
                ViewBag.Error = strError;
                return Json(new { Success = false, Message = strError }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                user.FullName = fullNameChange;
                user.Email = emailChange;
                user.Phone = phoneChange;
                user.Address = addressChange;
                user.UpdatedAt = DateTime.Now;
                user.UpdatedBy = (Session["CustomerId"].Equals("")) ? 1 : int.Parse(Session["CustomerId"].ToString());
                userDAO.Update(user);
                strError = "Thay đổi thông tin thành công!";
                ViewBag.Error = strError;
                return Json(new { Success = true, Message = strError }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult ThongTinCaNhan()
        {
            if (!Session["CustomerId"].Equals(""))
            {
                int userid = int.Parse(Session["CustomerId"].ToString());//Mã người đăng nhập

                User user = userDAO.getRow(userid);
                ViewBag.DetailInfoCus = user;

                //ViewBag.DetailInfoCus = userDAO.getList(user.Id);

                return View("ThongTinCaNhan");
            }
            else
            {
                return Redirect("~/");
            }
        }
        [HttpPost]
        public JsonResult UpdateImgCus(string imgUpdate,/* HttpPostedFileBase updateImgCus,*/ FormCollection field)
        {
            int userid = int.Parse(Session["CustomerId"].ToString());//Mã người đăng nhập

            User user = userDAO.getRow(userid);

            string strError = "";
            //string imgChageNew = "";
            ////updateImgCus.FileName = imgUpdate;
            //string fileNameChange = Path.GetFileName(imgUpdate).ToString();
            //if (updateImgCus != null) //check updateImgCus có giá trị k
            //{
            //    string fileName = "";
            //    string extension = Path.GetExtension(fileNameChange);
            //    fileName = user.UserName + extension;

            //    imgChageNew += fileName;
            //    string pathDir = "~/Public/images/users/";
            //    updateImgCus.SaveAs(Path.Combine(Server.MapPath(pathDir), fileName)); //Hình như là oke
            //}

            var fileImg = Request.Files["imageCusUpdate"];
            if (fileImg.ContentLength != 0)//Có chọn file
            {
                string[] FileExtensions = new string[] { ".jpeg", ".jpg", ".png", ".gif", ".jepg" };
                if (FileExtensions.Contains(fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."))))
                {
                    string imgName = user.FullName + fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."));
                    user.Images = imgName;
                    string pathDir = "~/Public/images/users/";
                    string pathImg = Path.Combine(Server.MapPath(pathDir), imgName);
                    //thực hiện upload
                    fileImg.SaveAs(pathImg);
                    //thực hiện lưu file
                    user.Images = imgName;
                }
            }
            string abc = imgUpdate;
            strError = "Thay đổi hình ảnh thành công";
            user.UpdatedAt = DateTime.Now;
            user.UpdatedBy = (Session["CustomerId"].Equals("")) ? 1 : int.Parse(Session["CustomerId"].ToString());
            userDAO.Update(user);
            return Json(new { Success = true, Message = strError, abc = fileImg }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ThongKeMuaHang()
        {
            if (!Session["CustomerId"].Equals(""))
            {
                int userid = int.Parse(Session["CustomerId"].ToString());//Mã người đăng nhập

                Order order = orderDAO.getRow(userid);
                OrderDetail orderDetail = orderdetailDAO.getRow(userid);

                ViewBag.order = orderDAO.getListOrderUserDetail(userid);
                ViewBag.detail = orderdetailDAO.getList(userid);

                return View("ThongKeMuaHang");
            }
            else
            {
                return Redirect("~/");
            }

        }
        public ActionResult DatHangThanhCong()
        {
            if (!Session["CustomerId"].Equals(""))
            {
                int userid = int.Parse(Session["CustomerId"].ToString());//Mã người đăng nhập

                int orderId = Convert.ToInt32(TempData["mydata"]);

                Order order = orderDAO.getRow(orderId);
                OrderDetail orderDetail = orderdetailDAO.getRow(orderId);

                ViewBag.order = orderDAO.getListOrderDetail(orderId);
                ViewBag.detail = orderdetailDAO.getList(orderId);

                OrderDetail orderDetail1 = new OrderDetail();
                int tg = orderDetail1.OrderId;
                if (tg != 0)
                {
                    ViewBag.ListChiTiet = orderdetailDAO.getList();
                }
                return View("DatHangThanhCong");
            }
            else
            {
                return Redirect("~/");
            }

        }
        public ActionResult InfoOrderClick(int id)
        {
            if (!Session["CustomerId"].Equals(""))
            {
                Order order = orderDAO.getRow(id);
                OrderDetail orderDetail = orderdetailDAO.getRow(id);

                ViewBag.order = orderDAO.getListOrderDetail(id);
                ViewBag.detail = orderdetailDAO.getList(id);

                return View("InfoOrderClick");
            }
            else
            {
                return Redirect("~/");
            }
        }
        public ActionResult HuyDonInfo(int? id)
        {
            Order order = orderDAO.getRow(id);
            if (order.Status == 1 || order.Status == 2)
            {
                order.Status = 0;
                order.UpdatedAt = DateTime.Now;
                order.UpdatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());

                TempData["message"] = new XMessage("success", "Đã hủy đơn hàng thành công");
                orderDAO.Update(order);
                TempData["message"] = new XMessage("success", "Đã hủy đơn hàng thành công");
                return View("ThongKeMuaHang");
            }
            else
            {
                if (order.Status == 3)
                {
                    TempData["message"] = new XMessage("danger", "Đơn hàng đã vận chuyển không thể hủy");
                }
                if (order.Status == 4)
                {
                    TempData["message"] = new XMessage("danger", "Đơn hàng đã giao không thể hủy");
                }
                return View("InfoOrderClick");
            }
        }
        public ActionResult HuyDon(int? id)
        {
            Order order = orderDAO.getRow(id);
            if (order.Status == 1 || order.Status == 2)
            {
                order.Status = 0;
                order.UpdatedAt = DateTime.Now;
                order.UpdatedBy = 1;
                List<CartItem> listCart = xCart.getCart();
                foreach (CartItem cartItem in listCart)
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.OrderId = order.Id;
                    orderDetail.ProductId = cartItem.ProductId;

                    Product product = productDAO.getRow(orderDetail.ProductId);
                    product.Number = product.Number + cartItem.QtyBuy;
                    productDAO.Update(product);
                }
            }
            else
            {
                if (order.Status == 3)
                {
                    TempData["message"] = new XMessage("danger", "Đơn hàng đã vận chuyển không thể hủy");
                }
                if (order.Status == 4)
                {
                    TempData["message"] = new XMessage("danger", "Đơn hàng đã giao không thể hủy");
                }
                return Redirect("dat-hang-thanh-cong");
            }
            TempData["message"] = new XMessage("success", "Đã hủy đơn hàng thành công");
            orderDAO.Update(order);
            return Redirect("thong-ke-mua-hang");
        }
        public String NameImgOrder(int? productId)
        {
            Product product = productDAO.getRow(productId);
            if (product == null)
            {
                return "";
            }
            else
            {
                return product.Images;
            }
        }
        public String NameProductOrder(int? productId)
        {
            Product product = productDAO.getRow(productId);
            if (product == null)
            {
                return "";
            }
            else
            {
                return product.Name;
            }
        }
    }
}