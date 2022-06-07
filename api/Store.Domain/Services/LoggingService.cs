using System;
using Microsoft.Extensions.Logging;
using Store.Domain.Services.Interfaces;

namespace Store.Domain.Services
{
    public class LoggingService : ILoggingService
    {
        private readonly ILogger<LoggingService> _logger;

        private int _requestNumber;
        private double _totalPauseDuration;

        public LoggingService(ILogger<LoggingService> logger)
        {
            _logger = logger;
            _requestNumber = 0;
            _totalPauseDuration = 0;
        }


        public void LogGCInfo()
        {
            var gcInfo = GC.GetGCMemoryInfo();
            _totalPauseDuration = GetTotalPauseDuration(gcInfo);
            long pinnedObjects = gcInfo.PinnedObjectsCount;
            long totalCommittedBytes = gcInfo.TotalCommittedBytes;
            long finalizationPendingCount = gcInfo.FinalizationPendingCount;
            long totalManagedMemory = GC.GetTotalMemory(false);

            _logger.LogInformation("#{0} - PauseDuration(ms): {1} - ManagedMemory(bytes): {2} - TotalCommittedBytes: {3} - FinalizationPendingCount: {4} - Gen 0: {5} - Gen 1: {6} - Gen 2: {7}", 
              ++_requestNumber, _totalPauseDuration, totalManagedMemory, totalCommittedBytes, finalizationPendingCount, GC.CollectionCount(0), GC.CollectionCount(1), GC.CollectionCount(2));
        }

        private double GetTotalPauseDuration(GCMemoryInfo gcInfo){
            var total = gcInfo.PauseDurations[0].TotalMilliseconds + gcInfo.PauseDurations[1].TotalMilliseconds;

            if(total == 0 && _totalPauseDuration == 0)
                return 0;
            else if(total == _totalPauseDuration)
                return _totalPauseDuration;
            else
                return _totalPauseDuration + total;
        }
    }
}