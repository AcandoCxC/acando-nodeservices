using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NodeServicesTest.Controllers
{
    using System.Threading.Tasks;
    using Acando.AspNet.NodeServices;
    using log4net;
    using log4net.Core;

    public class ReactResult : ActionResult
    {
        private string Data { get; }

        public ReactResult(string data)
        {
            Data = data;
        }
        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Write(Data);
        }
    }

    public class HomeController : Controller
    {
        private static readonly ILog logger = 
            LogManager.GetLogger(typeof(HomeController));
        
        public async Task<ActionResult> Index()
        {
            var node = NodeServicesFactory.CreateNodeServices(new NodeServicesOptions(logger, Server.MapPath("./")));
            var result = await node.InvokeAsync<string>("./app.server", "Server render me, biatch");

            return new ReactResult(result);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}