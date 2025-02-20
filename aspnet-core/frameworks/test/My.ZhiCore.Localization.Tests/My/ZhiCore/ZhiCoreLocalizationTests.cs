using My.ZhiCore.Localization;
using Microsoft.Extensions.Localization;
using Shouldly;
using Volo.Abp.Localization;
using Xunit;

namespace My.ZhiCore
{
    public sealed class ZhiCoreLocalizationTests : ZhiCoreLocalizationTestBase
    {
        private readonly IStringLocalizer<ZhiCoreLocalizationResource> _stringLocalizer;

        public ZhiCoreLocalizationTests()
        {
            _stringLocalizer = GetRequiredService<IStringLocalizer<ZhiCoreLocalizationResource>>();
        }

        [Fact]
        public void Test()
        {
            using (CultureHelper.Use("en"))
            {
                _stringLocalizer["Welcome"].Value.ShouldBe("Welcome");
                _stringLocalizer[ZhiCoreLocalizationErrorCodes.ErrorCode100001].Value.ShouldBe("The start page must be greater than or equal to 1");
                _stringLocalizer[ZhiCoreLocalizationErrorCodes.ErrorCode100003,"Name"].Value.ShouldBe("Name can not be empty");
            }

            using (CultureHelper.Use("zh-Hans"))
            {
                _stringLocalizer["Welcome"].Value.ShouldBe("欢迎");
                _stringLocalizer[ZhiCoreLocalizationErrorCodes.ErrorCode100001].Value.ShouldBe("起始页必须大于等于1");
                _stringLocalizer[ZhiCoreLocalizationErrorCodes.ErrorCode100003,"Name"].Value.ShouldBe("Name不能为空");
            }
        }
    }
}