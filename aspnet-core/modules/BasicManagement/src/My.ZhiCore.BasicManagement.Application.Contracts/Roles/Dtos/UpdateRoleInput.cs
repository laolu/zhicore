using Volo.Abp.Identity;

namespace My.ZhiCore.BasicManagement.Roles.Dtos
{
    public class UpdateRoleInput
    {
        public Guid RoleId { get; set; }

        public IdentityRoleUpdateDto RoleInfo { get; set; }
    }
}