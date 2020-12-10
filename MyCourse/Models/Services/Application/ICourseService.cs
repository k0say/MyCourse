﻿using MyCourse.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCourse.Models.Services.Application
{
    public interface ICourseService
    {
        Task<List<CourseViewModel>> GetCoursesAsync(string search, int page, string orderby, bool ascending);
        Task<CourseDetailViewModel> GetCourseAsync(int id);
    }
}
