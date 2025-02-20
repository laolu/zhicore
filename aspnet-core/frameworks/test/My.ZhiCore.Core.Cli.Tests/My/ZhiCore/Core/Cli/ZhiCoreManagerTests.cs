using My.ZhiCore.Cli.Github;
using Shouldly;
using Xunit;

namespace My.ZhiCore.Core.Cli;

public sealed class ZhiCoreManagerTests : ZhiCoreCoreCliTestBase
{
    private readonly IZhiCoreManager _abpProManager;

    public ZhiCoreManagerTests()
    {
        _abpProManager = GetRequiredService<IZhiCoreManager>();
    }

    [Fact]
    public async Task GetLatestSourceCodeVersionAsync()
    {
       var result= await _abpProManager.GetLatestSourceCodeVersionAsync();
       result.ShouldBe("7.2.2.3");
    }
    
    [Fact]
    public async Task CheckSourceCodeVersionAsync()
    {
        var result= await _abpProManager.CheckSourceCodeVersionAsync("7.2.2.3");
        result.ShouldBe(true);
        
        var result1= await _abpProManager.CheckSourceCodeVersionAsync("1.2.2.3");
        result1.ShouldBe(false);
    }
}