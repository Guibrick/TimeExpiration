using System.Threading.Tasks;

namespace TimeExpired.Services
{
  public interface IDateFileClient
  {
    Task<DateTime> AddDate(int dateId, DateTime date);
    Task<string> GetDateInput(int dateId);
  }
}