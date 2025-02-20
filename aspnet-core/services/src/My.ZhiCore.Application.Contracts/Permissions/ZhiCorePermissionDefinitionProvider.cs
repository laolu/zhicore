namespace My.ZhiCore.Permissions
{
    public class ZhiCorePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<ZhiCoreResource>(name);
        }
    }
}