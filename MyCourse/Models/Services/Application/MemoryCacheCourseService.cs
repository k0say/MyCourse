using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MyCourse.Models.Options;
using MyCourse.Models.Services.Infrastructure;
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
        
        public Task<CourseDetailViewModel> GetCourseAsync(int id)
        {
            //Prende il valore dall'appConfigure
            double time = expTime.CurrentValue.Default;

            return memoryCache.GetOrCreateAsync($"Course{id}", cacheEntry =>
            {
                cacheEntry.SetSize(2);
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(time));
                return courseService.GetCourseAsync(id);
            });
        }

        public Task<List<CourseViewModel>> GetCoursesAsync()
        {
            double time = expTime.CurrentValue.Default;

            return memoryCache.GetOrCreateAsync($"Courses", cacheEntry =>
            {
                cacheEntry.SetSize(2);
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(time));
                return courseService.GetCoursesAsync();
            });
        }
    }
}