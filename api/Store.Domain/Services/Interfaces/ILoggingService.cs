using Store.Domain.Customers.Dtos;
using Store.Domain.Dtos;
using System.Threading.Tasks;

namespace Store.Domain.Services.Interfaces
{
    public interface ILoggingService
    {
      void LogGCInfo();
    }
}