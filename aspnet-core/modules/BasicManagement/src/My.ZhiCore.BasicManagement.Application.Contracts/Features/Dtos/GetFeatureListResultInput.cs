namespace My.ZhiCore.BasicManagement.Features.Dtos;

public class GetFeatureListResultInput : IValidatableObject
{
    public string ProviderName { get; set; }

    public string ProviderKey { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var localization = validationContext.GetRequiredService<IStringLocalizer<ZhiCoreLocalizationResource>>();
        if (ProviderName.IsNullOrWhiteSpace())
        {
            yield return new ValidationResult(
                localization[ZhiCoreLocalizationErrorCodes.ErrorCode100003, nameof(ProviderName)],
                new[] { nameof(ProviderName) }
            );
        }
    }
}