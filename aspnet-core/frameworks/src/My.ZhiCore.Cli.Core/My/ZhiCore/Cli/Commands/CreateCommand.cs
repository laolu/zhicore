using My.ZhiCore.Cli.Auth;
using DirectoryHelper = My.ZhiCore.Cli.Utils.DirectoryHelper;

namespace My.ZhiCore.Cli.Commands;

public class CreateCommand : IConsoleCommand, ITransientDependency
{
    public const string Name = "create";
    private readonly ILogger<CreateCommand> _logger;
    private readonly ITokenAuthService _tokenAuthService;
    private readonly Options.ZhiCoreCliBusinessOptions _cliOptions;
    private readonly IGithubClient _githubClient;

    public CreateCommand(ILogger<CreateCommand> logger, ITokenAuthService tokenAuthService, IOptions<ZhiCoreCliBusinessOptions> cliOptions, IGithubClient githubClient)
    {
        _logger = logger;
        _tokenAuthService = tokenAuthService;
        _githubClient = githubClient;
        _cliOptions = cliOptions.Value;
    }

    public async Task ExecuteAsync(CommandLineArgs commandLineArgs)
    {
        _logger.LogInformation($"开始创建模板......");

        #region 参数判断

        // 判断是否输入token
        var token = await _tokenAuthService.GetAsync();
        if (token.IsNullOrWhiteSpace())
        {
            _logger.LogError("请登录cli, lion.abp login -token 你的token");
            return;
        }

        // 检查模板是否正确
        var template = commandLineArgs.Options.GetOrNull(CommandOptions.Template.Short, CommandOptions.Template.Long);
        var allTemplates = _cliOptions.Templates.Select(e => e.Key).JoinAsString("|");
        if (template.IsNullOrWhiteSpace())
        {
            _logger.LogError($"请输入模板名称,lion.abp create -t 模板名称({allTemplates})");
            GetUsageInfo();
            return;
        }

        var templateOptions = _cliOptions.Templates.FirstOrDefault(e => e.Key == template);
        if (templateOptions == null)
        {
            _logger.LogError($"模板类型不正确,lion.abp create -t 模板名称({allTemplates})");
            GetUsageInfo();
            return;
        }

        //校验是否输入公司名称
        var companyName = commandLineArgs.Options.GetOrNull(CommandOptions.Company.Short, CommandOptions.Company.Long);
        if (companyName.IsNullOrWhiteSpace())
        {
            _logger.LogError("请输入公司名称lion.abp create -c 公司名称");
            GetUsageInfo();
            return;
        }

        //校验是否输入项目名称
        var projectName = commandLineArgs.Options.GetOrNull(CommandOptions.Project.Short, CommandOptions.Project.Long);
        if (projectName.IsNullOrWhiteSpace())
        {
            _logger.LogError("请输入公司名称lion.abp create -p 项目名称");
            GetUsageInfo();
            return;
        }

        var version = commandLineArgs.Options.GetOrNull(CommandOptions.Version.Short, CommandOptions.Version.Long);
        var output = commandLineArgs.Options.GetOrNull(CommandOptions.Output.Short, CommandOptions.Output.Long);

        #endregion

        // 获取release信息
        Release release = null;
        if (version.IsNullOrWhiteSpace())
        {
            release = await _githubClient.GetLatestVersionAsync(_cliOptions.Owner, _cliOptions.RepositoryId, token);
            version = release.TagName;
        }
        else
        {
            release = await _githubClient.CheckVersionAsync(_cliOptions.Owner, _cliOptions.RepositoryId, token, version);
        }

        // 下载源码
        var localFilePath = Path.Combine(CliPaths.Source, $"{_cliOptions.RepositoryId}-{release.TagName}.zip");

        if (!Directory.Exists(CliPaths.Source))
        {
            Directory.CreateDirectory(CliPaths.Source);
        }

        if (!File.Exists(localFilePath))
        {
            _logger.LogInformation("正在从github下载源码......");
            await _githubClient.DownloadAsync(release.ZipballUrl, localFilePath, token);
            _logger.LogInformation("github源码下载完成.");
        }

        // 解压源码
        var extractPath = ZipHelper.Extract(localFilePath, _cliOptions.RepositoryId, version);

        var contentPath = templateOptions.Name == "pro" ? extractPath : Path.Combine(extractPath, ".templates", templateOptions.Name);
        if (output.IsNullOrWhiteSpace())
        {
            // 复制源码到输出目录
            output = Path.Combine(CliPaths.Output, $"{companyName}{projectName}{version}"); 
        }
        else
        {
            output = Path.Combine(output, $"{companyName}{projectName}{version}");
        }
        
     

        DirectoryHelper.CopyFolder(contentPath, output, templateOptions.ExcludeFiles);

        ReplaceHelper.ReplaceTemplates(
            output,
            templateOptions.OldCompanyName,
            templateOptions.OldProjectName,
            templateOptions.OldModuleName,
            companyName,
            projectName,
            string.Empty,
            templateOptions.ReplaceSuffix,
            version);

        _logger.LogInformation($"创建模板成功,请查阅----->: {output}");

        ProcessHelper.OpenExplorer(output);
    }


    public void GetUsageInfo()
    {
        var sb = new StringBuilder();
        sb.AppendLine("");
        sb.AppendLine("Usage:");
        sb.AppendLine("  lion.abp create");
        sb.AppendLine("lion.abp create -t 模板名称(source | nuget) -c 公司名称 -p 项目名称");
        _logger.LogInformation(sb.ToString());
    }

    public string GetShortDescription()
    {
        return "创建商业版本项目:lion.abp create -t 模板名称(source | nuget) -c 公司名称 -p 项目名称";
    }
}