namespace My.ZhiCore.BasicManagement.OrganizationUnits.Dto;

public class GetUnAddUserInput : PagingBase
{
    public Guid OrganizationUnitId { get; set; }

    public string Filter { get; set; }
}