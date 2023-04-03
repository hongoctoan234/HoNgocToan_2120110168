using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;

namespace ThietBiDienTu.Controllers
{
    public class GioHangController : Controller
    {
        ProductDAO productDAO = new ProductDAO();
        UserDAO userDAO = new UserDAO();
        OrderDAO orderDAO = new OrderDAO();
        OrderDetailDAO orderdetailDAO = new OrderDetailDAO();
        XCart xCart = new XCart();
        // GET: Cart
        public ActionResult Index()
        {
            //Lấy ra giỏ hàng
            List<CartItem> listCart = xCart.getCart();
            return View("Index", listCart);
        }
        public ActionResult AddCart(int productId)
        {
            Product product = productDAO.getRow(productId); //Lấy ra chi tiết sản phẩm
            CartItem cartItem = new CartItem(product.Id, product.Name, product.Images, product.PriceSale, 1);

            //Thêm vào giỏ hàng
            List<CartItem> listCart = xCart.AddCart(cartItem, productId);

            return Json(listCart);
        }
        public ActionResult AddCartDetail(int productId, int qty)
        {
            Product product = productDAO.getRow(productId); //Lấy ra chi tiết sản phẩm
            CartItem cartItem = new CartItem(product.Id, product.Name, product.Images, product.PriceSale, qty);

            //Thêm vào giỏ hàng
            List<CartItem> listCart;
            if (System.Web.HttpContext.Current.Session["MyCart"].Equals(""))
            {
                //Giỏ hàng đang trống
                listCart = new List<CartItem>();
                listCart.Add(cartItem);
                System.Web.HttpContext.Current.Session["MyCart"] = listCart;
            }
            else
            {
                //Giỏ hàng không trống
                listCart = (List<CartItem>)System.Web.HttpContext.Current.Session["MyCart"]; //ép kiểu
                //Kiểm tra productId đã có trong giỏ hàng chưa?
                if (listCart.Where(m => m.ProductId == productId).Count() != 0)
                {
                    //ProductId đã có trong giỏ hàng
                    cartItem.QtyBuy+= qty;
                    int vt = 0;
                    foreach (var item in listCart)
                    {
                        if (item.ProductId == productId)
                        {
                            listCart[vt].QtyBuy += qty;
                            listCart[vt].Amount = listCart[vt].PriceBuy * listCart[vt].QtyBuy;
                        }
                        vt++;
                    }
                    System.Web.HttpContext.Current.Session["MyCart"] = listCart;
                }
                //else
                //{
                //    //ProductId chưa có trong giỏ hàng
                //    listCart.Add(cartItem);
                //    System.Web.HttpContext.Current.Session["MyCart"] = listCart;
                //}
            }
            return Json(listCart);
        }
        public ActionResult DelCart(int productId)
        {
            xCart.DeleteCart(productId);
            return RedirectToAction("Index", "GioHang");
        }
        public ActionResult UpdateCart(FormCollection form)
        {
            if (!string.IsNullOrEmpty(form["clickUpdate"]))
            {
                var listqty = form["quantity"];
                var listarr = listqty.Split(',');
                xCart.UpdateCart(listarr);
            }
            return RedirectToAction("Index", "Giohang");
        }
        //[HttpPost]
        //public JsonResult UpdateCart(int qty, int vitri)
        //{
        //    //string strError = "";
        //    //if (!string.IsNullOrEmpty(form["clickUpdate"]))
        //    //{
        //    //    var listQty = form["qty"];
        //    //    var listArr = listQty.Split(',');
        //    //    xCart.UpdateCart(listArr);
        //    //}
        //    //strError = vitri.ToString() + qty.ToString();

        //    //var listQty = qty;
        //    List<CartItem> listCart = xCart.getCart();
        //    int vt = vitri;
        //    Product product = productDAO.getRow(listCart[vt].ProductId);

        //    if (product.Number <= listCart[vt].QtyBuy)
        //    {
        //        return Json(new { Success = false, Cart = listCart }, JsonRequestBehavior.AllowGet);
        //    }

        //    xCart.UpdateCart(qty, vitri);
        //    //update xong tra ve cart 
        //    listCart = (List<CartItem>)System.Web.HttpContext.Current.Session["MyCart"];
        //    //tim cart roi tra ve json
        //    return Json(new { Success = true, Cart = listCart }, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult DelAllCart()
        {
            xCart.DelAllCart();
            return RedirectToAction("Index", "GioHang");

        }

        //Thanh toán đơn hàng
        public ActionResult ThanhToan()
        {
            //Lấy ra giỏ hàng
            List<CartItem> listCart = xCart.getCart();
            //Kiểm tra đăng nhập trang người dùng
            if (Session["UserCustomer"].Equals(""))
            {
                return Redirect("~/login");//Chuyển đến trang đăng nhập
            }

            int userId = int.Parse(Session["CustomerId"].ToString()); //Lấy ra thông tin đăng nhập
            User user = userDAO.getRow(userId);
            ViewBag.User = user;

            return View("ThanhToan", listCart);

        }
        //Đặt mua đơn hàng
        [HttpPost]
        public ActionResult DatMua(FormCollection field)
        {
            if (Session["CustomerId"].Equals(""))
            {
                return Redirect("~/login");//Chuyển đến trang đăng nhập
            }
            string email = "";
            string name = "";
            string address = "";
            string phone = "";
            string notemail = "";
            //Lưu thông tin vào csdl
            int userId = int.Parse(Session["CustomerId"].ToString()); //Lấy ra thông tin đăng nhập
            User user = userDAO.getRow(userId);
            Order order = new Order();

            if (field["deliveryName"].Equals(""))
            {
                //Lấy thông tin
                string note = field["deliveryNote"];
                //Tạo đối tượng đơn hàng
                order.DeliveryNote = note;
                //-----------
                name = user.FullName;
                email = user.Email;
                address = user.Address;
                phone = user.Phone;
                notemail = note;
            }
            else
            {
                string deliveryName = field["deliveryName"];
                string deliveryEmail = field["deliveryEmail"];
                string deliveryAddress = field["deliveryAddress"];
                string deliveryPhone = field["deliveryPhone"];
                string note = field["deliveryNote"];
                //Tạo đối tượng đơn hàng
                order.DeliveryName = deliveryName;
                order.DeliveryEmail = deliveryEmail;
                order.DeliveryAddress = deliveryAddress;
                order.DeliveryPhone = deliveryPhone;
                order.DeliveryNote = note;
                //-----------
                name = deliveryName;
                email = deliveryEmail;
                address = deliveryAddress;
                phone = deliveryPhone;
                notemail = note;

            }
            order.UserId = userId;
            order.Status = 1;

            var chars = "0123456789";
            var stringChars = new char[15];
            var random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] += chars[random.Next(chars.Length)];
            }
            var finalString = new String(stringChars);

            order.Code = finalString;
            order.UpdatedBy = (Session["CustomerId"].Equals("")) ? 1 : int.Parse(Session["CustomerId"].ToString());
            order.UpdatedAt = DateTime.Now;
            order.ExportDate = DateTime.Now;
            order.CreateDate = DateTime.Now;
            decimal tongTien = 0;

            if (orderDAO.Insert(order) == 1)
            {
                //Thêm vào chi tiết đơn hàng
                List<CartItem> listCart = xCart.getCart();
                foreach (CartItem cartItem in listCart)
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.OrderId = order.Id;
                    orderDetail.ProductId = cartItem.ProductId;
                    orderDetail.Price = cartItem.PriceBuy;
                    orderDetail.Amount = cartItem.Amount;
                    tongTien += cartItem.Amount;

                    orderDetail.Quantity = cartItem.QtyBuy;
                    Product product = productDAO.getRow(orderDetail.ProductId);
                    product.Number = product.Number - cartItem.QtyBuy;
                    productDAO.Update(product);

                    orderdetailDAO.Insert(orderDetail);
                }


                MailMessage mail = new MailMessage();
                // you need to enter your mail address
                mail.From = new MailAddress("vietnamwatches.info@gmail.com");

                //To Email Address - your need to enter your to email address
                mail.To.Add(email);

                mail.Subject = "Đơn hàng mới từ ThietBiDienTu";

                //you can specify also CC and BCC - i will skip this
                //mail.CC.Add("");
                //mail.Bcc.Add("");

                mail.IsBodyHtml = true;
                //Mail template
                #region
                string content = "<div class=''style='width:80%;margin-left:10%'><div class='aHl'></div><div id=':ot' tabindex='-1'></div><div id=':oi' class='ii gt' jslog='20277; u014N:xr6bB; 4:W251bGwsbnVsbCxbXV0.'><div id=':oh' class='a3s aiL msg7831722724794612886'><div class='adM'>";
                content += "</div><u></u>";
                content += "<div style='margin:0px;padding:0px;color:rgb(32,32,32);font-size:14px;font-weight:normal;font-family:Helvetica,Arial,sans-serif!important;line-height:150%!important'>";
                content += "<div style='display:none!important;opacity:0;color:transparent;height:0;width:0'> +Đơn hàng của bạn #" + finalString + "</div>";
                content += "<div class='m_7831722724794612886main-content' align='center'>";
                content += "<table border='0' cellpadding='0' cellspacing='0' width='100%'>";
                content += "<tbody><tr>";
                content += "<td colspan='2'></td>";
                content += "<td bgcolor='#E8E8E8' colspan='3' height='1px'></td>";
                content += "<td colspan='3'></td>";
                content += "</tr>";
                content += "<tr>";
                content += "<td bgcolor='#F8F8F8' width='1px'></td>";
                content += "<td bgcolor='#E8E8E8' width='1px'></td>";
                content += "<td bgcolor='#D1D1D1' width='1px'></td>";
                content += "<td bgcolor='#FFF'>";
                content += "<div class='m_7831722724794612886header' style='margin-bottom:15px'>";
                content += "<div><table class='m_7831722724794612886header' lang='header' cellpadding='0' cellspacing='0' width='100%' border='0' style='width:100%'>";
                content += "<tbody><tr>";
                content += "<td width='100%' height='70' valign='top' bgcolor='#FFFFFF' style='padding-top:30px;background:#ffffff;height:70px'>";
                content += "<table cellpadding='0' cellspacing='0' width='100%' height='70' border='0' style='width:100%;height:70px'>";
                content += "</td></tr>";
                content += "<tr>";
                content += "<td width='100%' height='50' valign='middle' bgcolor='#FFFFFF' style='background:#ffffff;height:50px'>";
                content += "<table width='100%' cellspacing='0' cellpadding='0' border='0'>";
                content += "<tbody><tr>";
                content += "<td align='center' width='150'>";
                content += "<p style='font-size:11pt;font-weight:bold;font-family:Arial,Helvetica,sans-serif;color:#0f146d;background-color:#ffffff'>";
                //content += "<a href='https://c.lazada.vn/t/c.VkePT?sub_id1=Trade&amp;sub_id2=VN_VOYAGER_Delivered&amp;sub_id3=20220528&amp;sub_id4=top-menu&amp;url=https%3A%2F%2Fwww.lazada.vn%2Fshop%2Fbach-hoa-lazada' style='text-decoration:none;font-weight:bold;color:#0f146d!important' target='_blank' data-saferedirecturl='https://www.google.com/url?q=https://c.lazada.vn/t/c.VkePT?sub_id1%3DTrade%26sub_id2%3DVN_VOYAGER_Delivered%26sub_id3%3D20220528%26sub_id4%3Dtop-menu%26url%3Dhttps%253A%252F%252Fwww.lazada.vn%252Fshop%252Fbach-hoa-lazada&amp;source=gmail&amp;ust=1655722264126000&amp;usg=AOvVaw3oCroGs6-X3DXFqAZUSwap'>Bách Hóa<br> Lazada</a>";
                content += "</p></td></tr></tbody></table></td></tr></tbody></table></div></div>";
                content += "<div class='m_7831722724794612886section' style='padding-top:0px'>	";
                content += "<div class='m_7831722724794612886header-title' style='color:#0f146d;text-align:center'>Đơn hàng của bạn đã được xác nhận thành công!</div>";
                content += "<p class='m_7831722724794612886header-subText'>(Lăn xuống để xem thêm)</p>";
                content += "<div class='m_7831722724794612886section-content'>";
                content += "<h2>" + name + " ơi, </h2>";
                content += "<p>ThietBiDienTu đã xác nhận cho bạn đầy đủ với các sản phẩm được liệt kê bên dưới theo đơn hàng&nbsp;<strong>#" + finalString + "</strong> của bạn,";
                content += "ThietBiDienTu hi vọng bạn hài lòng với các sản phẩm này!</p>";
                content += "<p><strong>Một vài lưu ý nhỏ cho bạn:</strong></p>";
                content += "<ul>";
                content += "<li>Hãy kiểm tra kỹ chất lượng sản phẩm bạn nhận được và giữ lại hóa đơn, hộp sản phẩm và phiếu bảo hành (nếu có) để trả hàng hoặc bảo hành khi cần thiết.</li>";
                content += "</ul></div> </div>";

                foreach (CartItem cartItem in listCart)
                {

                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.OrderId = order.Id;
                    orderDetail.ProductId = cartItem.ProductId;
                    orderDetail.Price = cartItem.PriceBuy;
                    orderDetail.Amount = cartItem.Amount;

                    orderDetail.Quantity = cartItem.QtyBuy;
                    Product product = productDAO.getRow(orderDetail.ProductId);
                    content += "<div class='m_7831722724794612886section' style='padding-bottom:0px'>";
                    content += "<div class='m_7831722724794612886section-content'>";
                    content += "<div class='m_7831722724794612886section-header m_7831722724794612886section-header--yourPackage'>Kiện Hàng</div>";
                    content += "<p style='padding-left:10px;margin-top:0px;margin-bottom:0px;color:#585858'>";
                    content += "Được bán bởi: ThietBiDienTu";
                    content += "</p>";
                    content += "<div class='m_7831722724794612886product' style='border-bottom:0px none'>";
                    content += "<table cellpadding='0' cellspacing='0' style='width:100%'>";
                    content += "<tbody><tr>";
                    content += "<td style='width:40%'>";
                    content += "<div style='padding-right:10px'>";
                    content += "<a href='#' target='_blank' data-saferedirecturl=''><img src='' style='width:100%;max-width:160px' class='CToWUd'></a>";
                    content += "</div></td>";
                    content += "<td style='width:60%'>";
                    content += "<div class='m_7831722724794612886product-productInfo-name'>";
                    content += "<a href='' target='_blank' data-saferedirecturl=''><span style='font-size:14px'>" + product.Name + "</span></a>";
                    content += "</div>";
                    content += "<div class='m_7831722724794612886product-productInfo-price'><span style='font-size:14px'>VND " + cartItem.PriceBuy + "</span></div>";
                    content += "<div class='m_7831722724794612886product-productInfo-subInfo'><span style='font-size:14px'>Số lượng: " + cartItem.QtyBuy + "</span></div";
                    content += "</td></tr> </tbody></table></div>";
                    content += "</div> </div>";
                }

                content += "<div class='m_7831722724794612886section' style='padding-top:0px'>";
                content += "<div class='m_7831722724794612886section-content'>";
                content += "<div class='m_7831722724794612886checkout'>";
                content += "<div class='m_7831722724794612886two_col'>";
                content += "<table cellpadding='0' cellspacing='0' class='m_7831722724794612886checkout-amount' style='border-bottom:1px solid #d8d8d8'>";
                content += "<tbody><tr>";
                content += "<td valign='top' style='color:#585858;width:80%;margin-left:10%'>Thành tiền:</td>";
                content += "<td align='right' valign='top'>VND</td>";
                content += "<td align='right' valign='top'>" + tongTien + "</td>";
                content += "</tr><tr>";
                content += "<td valign='top' style='color:#585858;width:80%;margin-left:10%'>Phí vận chuyển:</td>";
                content += "<td align='right' valign='top'>VND </td>";
                if (tongTien < 500000)
                {
                    content += "<td align='right' valign='top'>" + 30.000 + "</td>";
                    tongTien += 30000;
                }
                else
                {
                    content += "<td align='right' valign='top'>  Miễn phí vận chuyển </td>";
                }
                content += "</tr><tr>";
                content += "<td valign='top' style='color:#585858;width:80%;margin-left:10%'>Tổng cộng:</td>";
                content += "<td align='right' valign='top'><div style='color:#f27c24;font-weight:bold'>VND</div></td>";
                content += "<td align='right' valign='top'><div style='color:#f27c24;font-weight:bold'>" + tongTien + "</div></td>";
                content += "</tr>";
                content += "</tbody></table>";
                content += "<br>";
                content += "<table cellpadding='0' cellspacing='0' class='m_7831722724794612886checkout-amount'>";
                content += "<tbody><tr>";
                content += "<td valign='top' style='color:#585858;width:49%'>Tùy chọn vận chuyển:</td>";
                content += "<td align='right' valign='top' colspan='2'>Giao hàng Tiêu chuẩn</td>";
                content += "</tr><tr>";
                content += "<td valign='top' style='color:#585858;width:49%'>Hình thức thanh toán:</td>";
                content += "<td align='right' valign='top' colspan='2'>Thanh toán khi nhận hàng</td>";
                content += "</tr></tbody></table></div></div></div></div>";
                content += "</td></tr></tbody></table></div>";

                #endregion
                //content += "<td colspan='3'></td>";
                //content += "<td colspan='3'></td>";
                //content += "<td colspan='3'></td>";


                mail.Body = content;

                //create SMTP instant

                //you need to pass mail server address and you can also specify the port number if you required
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");//google hay gmail ?

                //Create nerwork credential and you need to give from email address and password
                NetworkCredential networkCredential = new NetworkCredential("vietnamwatches.info@gmail.com", "wqqsugcuqcijsfaj");
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = networkCredential;
                smtpClient.Port = 587; // this is default port number - you can also change this
                smtpClient.EnableSsl = true; // if ssl required you need to enable it
                smtpClient.Send(mail);

            }
            ViewBag.OrderId = order.Id;
            TempData["mydata"] = order.Id;
            xCart.DelAllCart();
            return Redirect("~/dat-hang-thanh-cong");//Chuyển đến trang đăng nhập
        }
    }
}


