namespace My.ZhiCore.CAP;

public class ZhiCoreCapPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var abpIdentityGroup = context.GetGroup(ZhiCoreCapPermissions.CapManagement.Default);

        abpIdentityGroup.AddPermission(ZhiCoreCapPermissions.CapManagement.Cap, L("Permission:Cap"), multiTenancySide: MultiTenancySides.Both);
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ZhiCoreLocalizationResource>(name);
    }
}