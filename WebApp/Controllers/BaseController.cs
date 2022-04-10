using Lib.Application;
using Microsoft.AspNetCore.Mvc;


namespace WebApp.Controllers
{
    public class BaseController : Controller
    {
        private readonly Facade __facadeApplication;

        public Facade FacadeApplication => __facadeApplication;

        public BaseController(Facade facadeApplication)
        {
            __facadeApplication = facadeApplication;
        }


    }
}
