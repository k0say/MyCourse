using Microsoft.AspNetCore.Mvc;
using MyCourse.Models.Services.Application;
using MyCourse.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCourse.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService courseService;
        public CoursesController(ICourseService courseService)
        {
            this.courseService = courseService;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Catalogo dei corsi!!";
            List<CourseViewModel> courses = await courseService.GetCoursesAsync();
            return View(courses);
        }
        public async Task<IActionResult> Detail(int id)
        {
            CourseDetailViewModel viewModel = await courseService.GetCourseAsync(id);
            ViewData["Title"] = viewModel.Title;
            return View(viewModel);
        }
        public IActionResult Search(string title)
        {
            return Content($"Hai cercato {title}");
        }
    }
}
