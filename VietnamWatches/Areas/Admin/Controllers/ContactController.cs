using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;

namespace ThietBiDienTu.Areas.Admin.Controllers
{
    public class ContactController : Controller
    {
        private TBDTDBContext db = new TBDTDBContext();

        // GET: Admin/Contact
        public ActionResult Index()
        {
            return View(db.Contacts.ToList());
        }

        // GET: Admin/Contact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Admin/Contact/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Contact/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FullName,Email,Phone,Title,Detail,ReplayDetail,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Status")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contact);
        }

        // GET: Admin/Contact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Admin/Contact/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {

                MailMessage mail = new MailMessage();
                // you need to enter your mail address
                mail.From = new MailAddress("vietnamwatches.info@gmail.com");

                //To Email Address - your need to enter your to email address
                mail.To.Add(contact.Email);

                mail.Subject = "Phản hồi mới từ ThietBiDienTu";

                //you can specify also CC and BCC - i will skip this
                //mail.CC.Add("");
                //mail.Bcc.Add("");

                mail.IsBodyHtml = true;
                //Mail template
                #region
                string content = "<div class=''style='width:80%;margin-left:10%'><div class='aHl'></div><div id=':ot' tabindex='-1'></div><div id=':oi' class='ii gt' jslog='20277; u014N:xr6bB; 4:W251bGwsbnVsbCxbXV0.'><div id=':oh' class='a3s aiL msg7831722724794612886'><div class='adM'>";
                content += "</div><u></u>";
                content += "<div style='margin:0px;padding:0px;color:rgb(32,32,32);font-size:14px;font-weight:normal;font-family:Helvetica,Arial,sans-serif!important;line-height:150%!important'>";
                content += "<div style='display:none!important;opacity:0;color:transparent;height:0;width:0'> +Ý kiến của bạn #" + contact.Phone + "</div>";
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
                content += "<div class='m_7831722724794612886header-title' style='color:#0f146d;text-align:center'>Phản hồi của bạn đã được ghi nhận thành công!</div>";
                content += "<p class='m_7831722724794612886header-subText'>(Lăn xuống để xem thêm)</p>";
                content += "<div class='m_7831722724794612886section-content'>";
                content += "<h2>" + contact.FullName + " ơi, </h2>";
                content += "<p>ThietBiDienTu đã xác nhận cho bạn đầy đủ với các phản hồi được liệt kê bên dưới theo số điện thoại gửi&nbsp;<strong>#" + contact.Phone + "</strong> của bạn,";
                content += "ThietBiDienTu hi vọng bạn hài lòng về phản hồi này!</p>";
                content += "</div> </div>";

                content += "<div class='m_7831722724794612886section' style='padding-bottom:0px'>";
                content += "<div class='m_7831722724794612886section-content'>";
                content += "<div class='m_7831722724794612886section-header m_7831722724794612886section-header--yourPackage'>Thông tin</div>";
                content += "<p style='padding-left:10px;margin-top:0px;margin-bottom:0px;color:#585858'>";
                content += "Được phản hồi bởi: ThietBiDienTu";
                content += "</p>";
                content += "<div class='m_7831722724794612886product' style='border-bottom:0px none'>";
                content += "<table cellpadding='0' cellspacing='0' style='width:100%'>";
                content += "<tbody><tr>";
                content += "<td style='width:20%'>";
                content += "<div style='padding-right:10px'>";
                content += "<a href='#' target='_blank' data-saferedirecturl=''><img src='' style='width:100%;max-width:160px' class='CToWUd'></a>";
                content += "</div></td>";
                content += "<td style='width:80%'>";
                content += "<div class='m_7831722724794612886product-productInfo-name'>";
                content += "<a href='' target='_blank' data-saferedirecturl=''><span style='font-size:14px'> Tiêu đề:" + contact.Title + "</span></a>";
                content += "</div>";
                content += "<div class='m_7831722724794612886product-productInfo-price'><span style='font-size:14px'>Nội dung ý kiến của bạn:" + contact.Detail + "</span></div>";
                content += "<div class='m_7831722724794612886product-productInfo-subInfo'><span style='font-size:14px'>Phản hồi từ ThietBiDienTu: " + contact.ReplayDetail + "</span></div";
                content += "</td></tr> </tbody></table></div>";
                content += "</div> </div>";
                content += "<br>";
                content += "<table cellpadding='0' cellspacing='0' class='m_7831722724794612886checkout-amount'>";
                content += "<tbody><tr>";
                content += "<td valign='top' style='color:#585858;width:80%'>Cảm ơn bạn về những phản hồi tích cực, ThietBiDienTu xin phép ghi nhận và sẽ khắc phục sớm nhất những phản hồi của quý khách hàng. Thân!</td>";
                content += "</tr>";
                content += "</tbody></table>";
                content += "</td></tr></tbody></table></div>";

                #endregion

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
                contact.Status = 2;

                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(contact);
        }

        // GET: Admin/Contact/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Admin/Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = db.Contacts.Find(id);
            db.Contacts.Remove(contact);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
