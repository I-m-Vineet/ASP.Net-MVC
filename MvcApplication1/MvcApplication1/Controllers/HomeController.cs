using MvcApplication1.CustomActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    
    public class HomeController : Controller
    {
        //1. Set the "mode" attribute of <customErrors> element inside <system.web> to "On"
        //2. In this case, MVC will display the default error page "Error.cshtml" inside Views/Shared folder
        [HandleError]
        public ActionResult UnhandledException()
        {
            throw new Exception("This is an exception from UnhandledException() action");
        }


        //this will redirect to Error page only if NullReferenceException or IndexOutOfRangeException is thrown
        //try this with the following URIs
        // /Home/HandleSpecificException
        // /Home/HandleSpecificException/3
        // /Home/HandleSpecificException/10
        [HandleError(ExceptionType=typeof(NullReferenceException))]
        [HandleError(ExceptionType = typeof(IndexOutOfRangeException))]
        //[HandleError(ExceptionType=typeof(InvalidCastException),View="mycustomerrorpage")]
        public ActionResult HandleSpecificException(int ?id)
        {
            if (!id.HasValue)
            {
                throw new NullReferenceException("NullReferenceException from HandleSpecificException()");
            }
            if (id < 5)
            {
                throw new IndexOutOfRangeException("IndexOutOfRangeException from HandleSpecificException()");
            }
            else
            {
                throw new InvalidCastException("InvalidCastException from HandleSpecificException()");
            }
        }


        //this will cache the output for 5 seconds
        [OutputCache(Duration = 5)]
        public string ServerTime()
        {
            return DateTime.Now.ToLongTimeString();
        }

        //[LogFilter]       //apply custom action filter
        [HandleError]
        public ActionResult LongRunningAction()
        {
            
            //simulate a delay
            System.Threading.Thread.Sleep(5000);

            int x = 10, y = 20;
            if (x < y)      //throw some exception based on some condition
            {
                throw new Exception("Some exception was thrown from this action");
            }
            return Content("This is result from LongRunningAction() action");
        }

        public ActionResult CallMe()
        {
            System.Threading.Thread.Sleep(5000);
            return Content("THIS  IS THE ACTUAL RESULT FROM  THE ACTION METHOD");
        }
    }
}
