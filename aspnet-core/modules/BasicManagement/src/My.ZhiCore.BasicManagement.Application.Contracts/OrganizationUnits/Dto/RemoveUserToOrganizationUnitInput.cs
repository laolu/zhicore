namespace My.ZhiCore.BasicManagement.OrganizationUnits.Dto;

public class RemoveUserToOrganizationUnitInput
{
    public Guid UserId { get; set; }
    
    public Guid OrganizationUnitId { get; set; }
}