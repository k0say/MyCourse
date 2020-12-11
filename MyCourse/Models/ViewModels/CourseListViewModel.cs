using MyCourse.Models.InputModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCourse.Models.ViewModels
{
    public class CourseListViewModel
    {
        public List<CourseViewModel> Courses { get; set; }
        public CourseListInputModel Input { get; set; }
    }
}
