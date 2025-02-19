namespace ZhiCore.Application.Common.Models;

public class SmsSettings
{
    public string Provider { get; set; } = string.Empty;
    public string AccessKeyId { get; set; } = string.Empty;
    public string AccessKeySecret { get; set; } = string.Empty;
    public string SignName { get; set; } = string.Empty;
    public string RegionId { get; set; } = string.Empty;
    public string EndPoint { get; set; } = string.Empty;
}