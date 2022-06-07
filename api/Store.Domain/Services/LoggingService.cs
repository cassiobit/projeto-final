using System;
using Microsoft.Extensions.Logging;
using Store.Domain.Services.Interfaces;

namespace Store.Domain.Services
{
    public class LoggingService : ILoggingService
    {
        private readonly ILogger<LoggingService> _logger;

        private int _requestNumber;

        public LoggingService(ILogger<LoggingService> logger)
        {
            _logger = logger;
            _requestNumber = 0;
        }


        public void LogGCInfo()
        {
            var gcInfo = GC.GetGCMemoryInfo();
            double totalPauseDuration = gcInfo.PauseDurations[0].TotalMilliseconds + gcInfo.PauseDurations[1].TotalMilliseconds;
            long finalizationPendingCount = gcInfo.FinalizationPendingCount;
            long totalManagedMemory = GC.GetTotalMemory(false);

            _logger.LogInformation("#{0} - PauseDuration(ms): {1} - ManagedMemory(bytes): {2} - FinalizationPendingCount: {3} - Gen 0: {4} - Gen 1: {5} - Gen 2: {6}", 
              ++_requestNumber, totalPauseDuration, totalManagedMemory, finalizationPendingCount, GC.CollectionCount(0), GC.CollectionCount(1), GC.CollectionCount(2));
        }
    }
}