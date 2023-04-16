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
            //Luu th�ng tin --> Trang quan tri
            Session["UserId"] = "";
            Session["UserAdmin"] = "";
            Session["FullName"] = "";
            //Session["ImageAdmin"] = "";
            Session["MyCart"] = "";

            //Luu th�ng tin --> Trang nguoi
            Session["CustomerId"] = "";
            Session["UserCustomer"] = "";
            Session["FullNameCustomer"] = "";
        }

    }
}
