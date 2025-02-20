namespace My.ZhiCore.BasicManagement.Tenants.Dtos
{
    public class UpdateTenantInput : IValidatableObject
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var localization = validationContext.GetRequiredService<IStringLocalizer<ZhiCoreLocalizationResource>>();
            if (Name.IsNullOrWhiteSpace())
            {
                yield return new ValidationResult(
                    localization[ZhiCoreLocalizationErrorCodes.ErrorCode100003, nameof(Name)],
                    new[] { nameof(Name) }
                );
            }
        }
    }
}