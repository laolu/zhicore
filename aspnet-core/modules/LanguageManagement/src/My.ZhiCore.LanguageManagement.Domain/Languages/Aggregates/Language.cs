namespace My.ZhiCore.LanguageManagement.Languages.Aggregates;

public class Language : FullAuditedAggregateRoot<Guid>, ILanguageInfo, IMultiTenant
{
    private Language()
    {
    }


    public Language(
        Guid id,
        string cultureName,
        string uiCultureName,
        string displayName,
        string flagIcon,
        bool isEnabled,
        bool isDefault,
        Guid? tenantId = null
    ) : base(id)
    {
        SetCultureName(cultureName);
        SetUiCultureName(uiCultureName);
        SetDisplayName(displayName);
        SetFlagIcon(flagIcon);
        SetEnabled(isEnabled);
        SetDefault(isDefault);
        SetTenantId(tenantId);
    }

    /// <summary>
    /// 租户id
    /// </summary>
    public Guid? TenantId { get; private set; }
    
    /// <summary>
    /// 语言名称
    /// </summary>
    public string CultureName { get; private set; }

    /// <summary>
    /// Ui语言名称
    /// </summary>
    public string UiCultureName { get; private set; }

    /// <summary>
    /// 显示名称
    /// </summary>
    public string DisplayName { get; private set; }

    /// <summary>
    /// 图标
    /// </summary>
    public string FlagIcon { get; private set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsEnabled { get; private set; }

    /// <summary>
    /// 是否默认语言
    /// </summary>
    public bool IsDefault { get; private set; }

    /// <summary>
    /// 设置语言名称
    /// </summary>        
    private void SetCultureName(string cultureName)
    {
        Guard.NotNullOrWhiteSpace(cultureName, nameof(cultureName), 128, 0);
        CultureName = cultureName;
    }

    /// <summary>
    /// 设置Ui语言名称
    /// </summary>        
    private void SetUiCultureName(string uiCultureName)
    {
        Guard.NotNullOrWhiteSpace(uiCultureName, nameof(uiCultureName), 128, 0);
        UiCultureName = uiCultureName;
    }

    /// <summary>
    /// 设置显示名称
    /// </summary>        
    private void SetDisplayName(string displayName)
    {
        Guard.NotNullOrWhiteSpace(displayName, nameof(displayName), 128, 0);
        DisplayName = displayName;
    }

    /// <summary>
    /// 设置图标
    /// </summary>        
    private void SetFlagIcon(string flagIcon)
    {
        FlagIcon = flagIcon;
    }

    private void SetEnabled(bool isEnabled)
    {
        IsEnabled = isEnabled;
    }

    public void SetTenantId(Guid? tenantId)
    {
        TenantId = tenantId;
    }
    
    /// <summary>
    /// 更新语言
    /// </summary> 
    public void Update(
        string cultureName,
        string uiCultureName,
        string displayName,
        string flagIcon,
        bool isEnabled = true,
        bool isDefault = false
    )
    {
        SetCultureName(cultureName);
        SetUiCultureName(uiCultureName);
        SetDisplayName(displayName);
        SetFlagIcon(flagIcon);
        SetEnabled(isEnabled);
        SetDefault(isDefault);
    }


    public void SetDefault(bool isDefault)
    {
        IsDefault = isDefault;
    }
}