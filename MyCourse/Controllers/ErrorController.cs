using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyCourse.Models.Services.Application;

namespace MyCourse.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ErrorService errorService;
        public ErrorController(ErrorService errorService)
        {
            this.errorService = errorService;

        }
        public IActionResult Index()
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var errorResponse = errorService.GetError(feature);
            return View(errorResponse.ErrorView, errorResponse);
        }
    }
}