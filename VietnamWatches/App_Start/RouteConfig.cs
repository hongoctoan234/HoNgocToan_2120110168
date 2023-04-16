using System.Web.Mvc;
using System.Web.Routing;

namespace ThietBiDienTu
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Khai báo URL cố định
            routes.MapRoute(
                name: "TatCaSanPham",
                url: "tat-ca-san-pham",
                defaults: new { controller = "Site", action = "Product", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "TatCaBaiViet",
                url: "tat-ca-bai-viet",
                defaults: new { controller = "Site", action = "Post", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "LienHe",
               url: "lien-he",
               defaults: new { controller = "LienHe", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "GioHang",
               url: "gio-hang",
               defaults: new { controller = "GioHang", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "ThanhToan",
               url: "thanh-toan",
               defaults: new { controller = "GioHang", action = "ThanhToan", id = UrlParameter.Optional }
           );
            routes.MapRoute(
              name: "TimKiem",
              url: "tim-kiem",
              defaults: new { controller = "TimKiem", action = "Index", id = UrlParameter.Optional }
          );

            routes.MapRoute(
              name: "GioiThieu",
              url: "gioi-thieu",
              defaults: new { controller = "GioiThieu", action = "Index", id = UrlParameter.Optional }
          );
            routes.MapRoute(
                name: "TatCaNhaCungCap",
                url: "tat-ca-nha-cung-cap",
                defaults: new { controller = "Site", action = "Supplier", id = UrlParameter.Optional }
            );
            routes.MapRoute(
              name: "DangNhap",
              url: "login",
              defaults: new { controller = "KhachHang", action = "DangNhap", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "DangKi",
              url: "register",
              defaults: new { controller = "KhachHang", action = "DangKi", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "DangXuat",
              url: "logout",
              defaults: new { controller = "KhachHang", action = "DangXuat", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "DatHangThanhCong",
              url: "dat-hang-thanh-cong",
              defaults: new { controller = "KhachHang", action = "DatHangThanhCong", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "ThongKeMuaHang",
              url: "thong-ke-mua-hang",
              defaults: new { controller = "KhachHang", action = "ThongKeMuaHang", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "ThongTinCaNhan",
              url: "thong-tin-ca-nhan",
              defaults: new { controller = "KhachHang", action = "ThongTinCaNhan", id = UrlParameter.Optional }
          );


            //Khai báo URL động- luôn nằm kế trên Default
            routes.MapRoute(
              name: "SiteSlug",
              url: "{slug}",
              defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
          );


            //===================================

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
