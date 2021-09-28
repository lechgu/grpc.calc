using Client;
using Grid;
using Grpc.Core;
using Grpc.Core.Interceptors;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
  


        [HttpPost]
        public async Task<ActionResult> Index(Calculation model)
        {
            var channel = new Channel("calculator.grid.demanddriventech.com", 443, ChannelCredentials.SecureSsl);
            //var interceptor = new RetryInterceptor();
            //var invoker = channel.Intercept(interceptor);

            var client = new Calculator.CalculatorClient(channel);
            var reply = await client.CalculateAsync(new CalculateRequest
            {
                X = model.X,
                Y = model.Y,
                Op = "+"
            });
            model.Result = (int)reply.Result;
            return View(model);
        }
    }

}