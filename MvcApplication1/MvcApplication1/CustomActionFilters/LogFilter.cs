using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace MvcApplication1.CustomActionFilters
{
    public class LogFilterAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        Stopwatch watch = new Stopwatch();
        StreamWriter sw = null;
        
        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Write("<br>Executing action method.....<br/>");
            sw = new StreamWriter(filterContext.HttpContext.Server.MapPath("~") + "time.txt");
            watch.Start();
            sw.WriteLine(filterContext.ActionDescriptor.ActionName + " action execution started at: "
                + DateTime.Now.ToString());

            
        }


        public override void OnActionExecuted(System.Web.Mvc.ActionExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Write("Action method executed.....<br/>");
            watch.Stop();
            sw.WriteLine(filterContext.ActionDescriptor.ActionName + " action execution finished at: "
                + DateTime.Now.ToString());

            sw.WriteLine("Total seconds: " + (watch.ElapsedMilliseconds / 1000).ToString());

            //check if an exception was thrown during execution of action
            //log exception details
            if (filterContext.Exception != null)    
            {
                sw.WriteLine("An exception also occured: " + filterContext.Exception.Message);
            }
            else
            {
                sw.WriteLine("Action method executed without any exception");
            }
            sw.Close();
        }

        public override void OnResultExecuting(System.Web.Mvc.ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Write("<br/>Executing ActionResult....<br/>");

            

            
        }

        public override void OnResultExecuted(System.Web.Mvc.ResultExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Write("<br/>ActionResult executed....");
        }
    }
}