using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MyCourse.Models.InputModels;
using MyCourse.Models.Options;
using MyCourse.Models.ViewModels;
namespace MyCourse.Models.Services.Application
{
    public class MemoryCacheCourseService : ICachedCourseService
    {
        private readonly ICourseService courseService;
        private readonly IMemoryCache memoryCache;
        private readonly IOptionsMonitor<ExpirationTimeOptions> expTime;

        public MemoryCacheCourseService(ICourseService courseService, IMemoryCache memoryCache, IOptionsMonitor<ExpirationTimeOptions> expTime)
        {
            this.courseService = courseService;
            this.memoryCache = memoryCache;
            this.expTime = expTime;
        }

        public Task<List<CourseViewModel>> GetMostRecentCoursesAsync()
        {
            double time = expTime.CurrentValue.Default;
            return memoryCache.GetOrCreateAsync($"MostRecentCourses", cacheEntry =>
            {
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(time));
                return courseService.GetMostRecentCoursesAsync();
            });
        }

        public Task<CourseDetailViewModel> GetCourseAsync(int id)
        {
            //Prende il valore dall'appConfigure
            double time = expTime.CurrentValue.Default;

            return memoryCache.GetOrCreateAsync($"Course{id}", cacheEntry =>
            {
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(time));
                return courseService.GetCourseAsync(id);
            });
        }

        public Task<ListViewModel<CourseViewModel>> GetCoursesAsync(CourseListInputModel model)
        {
            double time = expTime.CurrentValue.Default;
            bool canCache = model.Page <= 5 && string.IsNullOrEmpty(model.Search);
            if (canCache)
            {
                return memoryCache.GetOrCreateAsync($"Courses{model.Search}-{model.Page}-{model.OrderBy}-{model.Ascending}", cacheEntry =>
                {
                    //cacheEntry.SetSize(1);
                    cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(time));
                    return courseService.GetCoursesAsync(model);
                });
            }
            return courseService.GetCoursesAsync(model);
        }

        public Task<List<CourseViewModel>> GetBestRatingCoursesAsync()
        {
            double time = expTime.CurrentValue.Default;
            return memoryCache.GetOrCreateAsync($"BestRatingCourses", cacheEntry =>
            {
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(time));
                return courseService.GetBestRatingCoursesAsync();
            });
        }
    }
}