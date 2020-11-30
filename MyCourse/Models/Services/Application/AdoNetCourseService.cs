using MyCourse.Models.Services.Infrastructure;
using MyCourse.Models.ViewModels;
using System.Collections.Generic;
using System.Data;

namespace MyCourse.Models.Services.Application
{
    public class AdoNetCourseService : ICourseService
    {
        private readonly IDatabaseAccessor db;
        public AdoNetCourseService(IDatabaseAccessor db)
        {

        }
        public CourseDetailViewModel GetCourse(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<CourseViewModel> GetCourses()
        {
            string query = "Select * from Courses";
            DataSet dataSet = db.Query(query);
            throw new System.NotImplementedException();
        }
    }
}
