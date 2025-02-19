using System.Threading.Tasks;

namespace ZhiCore.Application.Common.Interfaces;

public interface ISmsService
{
    Task SendSmsAsync(string phoneNumber, string templateCode, Dictionary<string, string> templateParams);
    Task SendVerificationCodeAsync(string phoneNumber, string code);
    Task SendNotificationAsync(string phoneNumber, string content);
    Task SendBatchSmsAsync(string[] phoneNumbers, string templateCode, Dictionary<string, string> templateParams);
}