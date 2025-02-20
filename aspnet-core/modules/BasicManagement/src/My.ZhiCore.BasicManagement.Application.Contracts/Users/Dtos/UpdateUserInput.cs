using Volo.Abp.Identity;

namespace My.ZhiCore.BasicManagement.Users.Dtos
{
    public class UpdateUserInput
    {
        public Guid UserId { get; set; }

        public IdentityUserUpdateDto UserInfo { get; set; }
    }
}
