using EcommerceBasket.Domain.Repositories;

using Microsoft.Extensions.Logging;

using Quartz;

namespace EcommerceBasket.Infrastructure.CronJobs
{
    public class DeleteOldBasketRecordsJob : IJob
    {
        private readonly IBasketRepository _basketRepository;
        private readonly ILogger<DeleteOldBasketRecordsJob> _logger;

        public DeleteOldBasketRecordsJob(IBasketRepository basketRepository, ILogger<DeleteOldBasketRecordsJob> logger)
        {
            _basketRepository = basketRepository;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Running DeleteOldBasketRecords Job...");
            var toDate = DateTime.UtcNow.AddDays(-7);
            var deletedRecordsCount = await _basketRepository.DeleteBasketRecordsToUpdatedAt(toDate);
            _logger.LogInformation("A total of {DeletedRecordsCount} basket records were deleted.", deletedRecordsCount);
        }
    }
}
