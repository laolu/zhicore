using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Core.Http;
using Microsoft.Extensions.Options;
using ZhiCore.Application.Common.Interfaces;
using ZhiCore.Application.Common.Models;

namespace ZhiCore.Infrastructure.Sms;

public class AliyunSmsService : ISmsService
{
    private readonly SmsSettings _smsSettings;
    private readonly IClientProfile _profile;
    private readonly IAcsClient _acsClient;

    public AliyunSmsService(IOptions<SmsSettings> smsSettings)
    {
        _smsSettings = smsSettings.Value;
        _profile = DefaultProfile.GetProfile(_smsSettings.RegionId, _smsSettings.AccessKeyId, _smsSettings.AccessKeySecret);
        _acsClient = new DefaultAcsClient(_profile);
    }

    public async Task SendSmsAsync(string phoneNumber, string templateCode, Dictionary<string, string> templateParams)
    {
        var request = new CommonRequest
        {
            Method = MethodType.POST,
            Domain = _smsSettings.EndPoint,
            Version = "2017-05-25",
            Action = "SendSms"
        };

        request.AddQueryParameters("PhoneNumbers", phoneNumber);
        request.AddQueryParameters("SignName", _smsSettings.SignName);
        request.AddQueryParameters("TemplateCode", templateCode);
        request.AddQueryParameters("TemplateParam", System.Text.Json.JsonSerializer.Serialize(templateParams));

        try
        {
            var response = await Task.FromResult(_acsClient.GetCommonResponse(request));
            var result = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(response.Data);

            if (result != null && result.TryGetValue("Code", out var code) && code != "OK")
            {
                throw new Exception($"Failed to send SMS: {result.GetValueOrDefault("Message")}");
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"SMS sending failed: {ex.Message}", ex);
        }
    }

    public async Task SendVerificationCodeAsync(string phoneNumber, string code)
    {
        var templateParams = new Dictionary<string, string> { { "code", code } };
        await SendSmsAsync(phoneNumber, "SMS_VERIFICATION_TEMPLATE", templateParams);
    }

    public async Task SendNotificationAsync(string phoneNumber, string content)
    {
        var templateParams = new Dictionary<string, string> { { "content", content } };
        await SendSmsAsync(phoneNumber, "SMS_NOTIFICATION_TEMPLATE", templateParams);
    }

    public async Task SendBatchSmsAsync(string[] phoneNumbers, string templateCode, Dictionary<string, string> templateParams)
    {
        var request = new CommonRequest
        {
            Method = MethodType.POST,
            Domain = _smsSettings.EndPoint,
            Version = "2017-05-25",
            Action = "SendBatchSms"
        };

        request.AddQueryParameters("PhoneNumberJson", System.Text.Json.JsonSerializer.Serialize(phoneNumbers));
        request.AddQueryParameters("SignName", _smsSettings.SignName);
        request.AddQueryParameters("TemplateCode", templateCode);
        request.AddQueryParameters("TemplateParamJson", System.Text.Json.JsonSerializer.Serialize(templateParams));

        try
        {
            var response = await Task.FromResult(_acsClient.GetCommonResponse(request));
            var result = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(response.Data);

            if (result != null && result.TryGetValue("Code", out var code) && code != "OK")
            {
                throw new Exception($"Failed to send batch SMS: {result.GetValueOrDefault("Message")}");
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Batch SMS sending failed: {ex.Message}", ex);
        }
    }
}