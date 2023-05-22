using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace web_chung_cu
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /* user */
            routes.MapRoute(
              name: "UserDelete",
              url: "quan-tri/quan-ly-tai-khoan/xoa/{id}",
              defaults: new { controller = "User", action = "Delete", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "UserEdit",
              url: "quan-tri/quan-ly-tai-khoan/chinh-sua/{id}",
              defaults: new { controller = "User", action = "Edit", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "UserAdd",
              url: "quan-tri/quan-ly-tai-khoan/them-moi",
              defaults: new { controller = "User", action = "Add" }
            );

            routes.MapRoute(
               name: "UserIndex",
               url: "quan-tri/quan-ly-tai-khoan",
               defaults: new { controller = "User", action = "Index" }
           );
            /* user */

            /* room */
            routes.MapRoute(
              name: "RoomDelete",
              url: "quan-tri/quan-ly-phong/xoa/{id}",
              defaults: new { controller = "Room", action = "Delete", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "RoomEdit",
              url: "quan-tri/quan-ly-phong/chinh-sua/{id}",
              defaults: new { controller = "Room", action = "Edit", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "RoomAdd",
              url: "quan-tri/quan-ly-phong/them-moi",
              defaults: new { controller = "Room", action = "Add" }
            );

            routes.MapRoute(
               name: "RoomIndex",
               url: "quan-tri/quan-ly-phong",
               defaults: new { controller = "Room", action = "Index" }
           );
            /* room */

            /* apartment */
            routes.MapRoute(
              name: "ApartmentDelete",
              url: "quan-tri/quan-ly-chung-cu/xoa/{id}",
              defaults: new { controller = "Apartment", action = "Delete", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "ApartmentEdit",
              url: "quan-tri/quan-ly-chung-cu/chinh-sua/{id}",
              defaults: new { controller = "Apartment", action = "Edit", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "ApartmentAdd",
              url: "quan-tri/quan-ly-chung-cu/them-moi",
              defaults: new { controller = "Apartment", action = "Add"}
            );

            routes.MapRoute(
               name: "ApartmentIndex",
               url: "quan-tri/quan-ly-chung-cu",
               defaults: new { controller = "Apartment", action = "Index" }
           );
           /* apartment */

            routes.MapRoute(
               name: "AdminIndex",
               url: "quan-tri",
               defaults: new { controller = "Admin", action = "Index" }
           );

            // auth

            routes.MapRoute(
                name: "Logout",
                url: "dang-xuat",
                defaults: new { controller = "Auth", action = "Logout" }
            );

            routes.MapRoute(
                name: "Register",
                url: "dang-ky",
                defaults: new { controller = "Auth", action = "Register"}
            );

            routes.MapRoute(
                name: "Login",
                url: "dang-nhap",
                defaults: new { controller = "Auth", action = "Login" }
            );

            // client
            routes.MapRoute(
               name: "HomeContact",
               url: "lien-he",
               defaults: new { controller = "Home", action = "Contact"}
            );

            routes.MapRoute(
               name: "HomeAbout",
               url: "gioi-thieu",
               defaults: new { controller = "Home", action = "About"}
            );

            routes.MapRoute(
               name: "HomeNews",
               url: "tin-tuc",
               defaults: new { controller = "Home", action = "News"}
            );

            routes.MapRoute(
               name: "HomeApartmentDetail",
               url: "thong-tin-chung-cu/{slug}",
               defaults: new { controller = "Home", action = "ApartmentDetail", slug = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "HomeApartment",
               url: "chung-cu",
               defaults: new { controller = "Home", action = "Apartment" }
            );

            routes.MapRoute(
               name: "Index",
               url: "",
               defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
               name: "ErrorNotFound",
               url: "*",
               defaults: new { controller = "Error", action = "NotFound" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
