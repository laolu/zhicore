using Volo.Abp.Identity;

namespace My.ZhiCore.BasicManagement.Users.Dtos;

public class PageIdentityUserOutput : IdentityUserDto
{
    /// <summary>
    /// 是否开启双因素验证码
    /// </summary>
    public bool TwoFactorEnabled { get; set; }
}