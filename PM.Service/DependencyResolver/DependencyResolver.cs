using Microsoft.Extensions.DependencyInjection;
using PM.Service.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PM.Service.DependencyResolver
{
    public static class DependencyResolver
    {
        public static void AddDependecies(this IServiceCollection services)
        {
            services.AddTransient<IPermitUsageService, PermitUsageService>();
            services.AddTransient<IMemberService, MemberService>();
            services.AddTransient<IPublicHolidayService, PublicHolidayService>();
        }

    }
}
