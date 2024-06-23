using EcommerceBasket.Infrastructure.CronJobs;

using Quartz;

namespace EcommerceBasket.API.Configuration
{
    public static class QuartzConfiguration
    {
        public static void ConfigureQuartz(this WebApplicationBuilder builder)
        {
            builder.Services.AddQuartz(q =>
            {
                var jobKey = new JobKey("DeleteOldBasketRecordsJob");
                q.AddJob<DeleteOldBasketRecordsJob>(opts => opts.WithIdentity(jobKey));

                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("deleteOldBasketRecords")
                    .WithCronSchedule("0 0 0 ? * *")
                );
            });
            builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        }
    }
}
