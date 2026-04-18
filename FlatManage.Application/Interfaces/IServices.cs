using System.Threading.Tasks;

namespace FlatManage.Application.Interfaces
{
    public interface ISmsService
    {
        Task<bool> SendSmsAsync(string phoneNumber, string message);
    }

    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string email, string subject, string body);
    }
}
