using System.Collections.Generic;

namespace MyCourse.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<CourseViewModel> MostRecentCourses { get; set; }
        public List<CourseViewModel> BestRatingCourses { get; set; }
    }
}