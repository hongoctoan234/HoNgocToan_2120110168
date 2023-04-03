using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ThietBiDienTu
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected void Session_Start()
        {
            //L?u thông tin --> Trang qu?n tr?
            Session["UserId"] = "";
            Session["UserAdmin"] = "";
            Session["FullName"] = "";
            //Session["ImageAdmin"] = "";
            Session["MyCart"] = "";

            //L?u thông tin --> Trang ng??i dùng
            Session["CustomerId"] = "";
            Session["UserCustomer"] = "";
            Session["FullNameCustomer"] = "";
        }

    }
}
