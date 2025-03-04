namespace My.ZhiCore.BasicManagement.OrganizationUnits.Dto;

public class UpdateOrganizationUnitInput : IValidatableObject
{
    public string DisplayName { get; set; }

    public Guid Id { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var localization = validationContext.GetRequiredService<IStringLocalizer<ZhiCoreLocalizationResource>>();
        if (DisplayName.IsNullOrWhiteSpace())
        {
            yield return new ValidationResult(
                localization[ZhiCoreLocalizationErrorCodes.ErrorCode100003, nameof(DisplayName)],
                new[] {  nameof(DisplayName) }
            );
        }
    }
}