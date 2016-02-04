using EgtMerkezi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EgtMerkezi.Attributes
{
    public class AdminRoleControl : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // action çalışmadan önce yapılacak işlemler

            var ent = new EgitimMerkeziEntities();
            if (HttpContext.Current.Session["eposta"] == null)
            {
                filterContext.HttpContext.Response.Redirect("~/Home/Register");
            }
            else
            {
                string Eposta = HttpContext.Current.Session["eposta"].ToString();
                var uye_rol = ent.Ogrenci.FirstOrDefault(x => x.Eposta == Eposta).Role;
                if (uye_rol != "Admin")
                {
                    filterContext.HttpContext.Response.Redirect("~/Home/Index");
                }
            }
            base.OnActionExecuting(filterContext);
        }

    }
}