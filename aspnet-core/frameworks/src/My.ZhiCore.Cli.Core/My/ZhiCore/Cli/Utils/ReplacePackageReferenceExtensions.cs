namespace My.ZhiCore.Cli.Utils;

public static class ReplacePackageReferenceExtensions
{
    public static string ReplacePackageReferenceCore(this string content)
    {
        return content
                .Replace("<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\frameworks\\src\\My.ZhiCore.Core\\My.ZhiCore.Core.csproj\"/>",
                    "<PackageReference Include=\"My.ZhiCore.Core\"/>")
                .Replace("<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\frameworks\\src\\My.ZhiCore.Core\\My.ZhiCore.Core.csproj\"/>",
                    "<PackageReference Include=\"My.ZhiCore.Core\"/>")
                .Replace("<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\shared\\My.ZhiCore.Shared.Hosting.Microservices\\My.ZhiCore.Shared.Hosting.Microservices.csproj\"/>",
                    "<PackageReference Include=\"My.ZhiCore.Shared.Hosting.Microservices\"/>")
                .Replace("<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\shared\\My.ZhiCore.Shared.Hosting.Gateways\\My.ZhiCore.Shared.Hosting.Gateways.csproj\"/>",
                    "<PackageReference Include=\"My.ZhiCore.Shared.Hosting.Gateways\"/>")
            ;
    }

    public static string ReplacePackageReferenceBasicManagement(this string content)
    {
        return content
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\My.ZhiCore.BasicManagement.Application\\My.ZhiCore.BasicManagement.Application.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.BasicManagement.Application\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\My.ZhiCore.BasicManagement.Application.Contracts\\My.ZhiCore.BasicManagement.Application.Contracts.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.BasicManagement.Application.Contracts\"/>")
            .Replace("<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\My.ZhiCore.BasicManagement.Domain\\My.ZhiCore.BasicManagement.Domain.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.BasicManagement.Domain\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\My.ZhiCore.BasicManagement.Domain.Shared\\My.ZhiCore.BasicManagement.Domain.Shared.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.BasicManagement.Domain.Shared\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\My.ZhiCore.BasicManagement.EntityFrameworkCore\\My.ZhiCore.BasicManagement.EntityFrameworkCore.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.BasicManagement.EntityFrameworkCore\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\My.ZhiCore.BasicManagement.FreeSqlRepository\\My.ZhiCore.BasicManagement.FreeSqlRepository.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.FreeSqlRepository\"/>")
            .Replace("<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\My.ZhiCore.BasicManagement.HttpApi\\My.ZhiCore.BasicManagement.HttpApi.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.BasicManagement.HttpApi\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\My.ZhiCore.BasicManagement.HttpApi.Client\\My.ZhiCore.BasicManagement.HttpApi.Client.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.BasicManagement.HttpApi.Client\"/>");
    }

    public static string ReplacePackageReferenceDataDictionaryManagement(this string content)
    {
        return content
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\My.ZhiCore.DataDictionaryManagement.Application\\My.ZhiCore.DataDictionaryManagement.Application.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.DataDictionaryManagement.Application\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\My.ZhiCore.DataDictionaryManagement.Application.Contracts\\My.ZhiCore.DataDictionaryManagement.Application.Contracts.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.DataDictionaryManagement.Application.Contracts\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\My.ZhiCore.DataDictionaryManagement.Domain\\My.ZhiCore.DataDictionaryManagement.Domain.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.DataDictionaryManagement.Domain\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\My.ZhiCore.DataDictionaryManagement.Domain.Shared\\My.ZhiCore.DataDictionaryManagement.Domain.Shared.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.DataDictionaryManagement.Domain.Shared\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\My.ZhiCore.DataDictionaryManagement.EntityFrameworkCore\\My.ZhiCore.DataDictionaryManagement.EntityFrameworkCore.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.DataDictionaryManagement.EntityFrameworkCore\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\My.ZhiCore.DataDictionaryManagement.FreeSqlRepository\\My.ZhiCore.DataDictionaryManagement.FreeSqlRepository.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.FreeSqlRepository\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\My.ZhiCore.DataDictionaryManagement.HttpApi\\My.ZhiCore.DataDictionaryManagement.HttpApi.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.DataDictionaryManagement.HttpApi\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\My.ZhiCore.DataDictionaryManagement.HttpApi.Client\\My.ZhiCore.DataDictionaryManagement.HttpApi.Client.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.DataDictionaryManagement.HttpApi.Client\"/>");
    }

    public static string ReplacePackageReferenceFileManagement(this string content)
    {
        return content
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\My.ZhiCore.FileManagement.Application\\My.ZhiCore.FileManagement.Application.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.FileManagement.Application\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\My.ZhiCore.FileManagement.Application.Contracts\\My.ZhiCore.FileManagement.Application.Contracts.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.FileManagement.Application.Contracts\"/>")
            .Replace("<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\My.ZhiCore.FileManagement.Domain\\My.ZhiCore.FileManagement.Domain.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.FileManagement.Domain\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\My.ZhiCore.FileManagement.Domain.Shared\\My.ZhiCore.FileManagement.Domain.Shared.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.FileManagement.Domain.Shared\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\My.ZhiCore.FileManagement.EntityFrameworkCore\\My.ZhiCore.FileManagement.EntityFrameworkCore.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.FileManagement.EntityFrameworkCore\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\My.ZhiCore.FileManagement.FreeSqlRepository\\My.ZhiCore.FileManagement.FreeSqlRepository.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.FreeSqlRepository\"/>")
            .Replace("<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\My.ZhiCore.FileManagement.HttpApi\\My.ZhiCore.FileManagement.HttpApi.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.FileManagement.HttpApi\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\My.ZhiCore.FileManagement.HttpApi.Client\\My.ZhiCore.FileManagement.HttpApi.Client.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.FileManagement.HttpApi.Client\"/>");
    }

    public static string ReplacePackageReferenceLanguageManagement(this string content)
    {
        return content
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\My.ZhiCore.LanguageManagement.Application\\My.ZhiCore.LanguageManagement.Application.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.LanguageManagement.Application\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\My.ZhiCore.LanguageManagement.Application.Contracts\\My.ZhiCore.LanguageManagement.Application.Contracts.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.LanguageManagement.Application.Contracts\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\My.ZhiCore.LanguageManagement.Domain\\My.ZhiCore.LanguageManagement.Domain.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.LanguageManagement.Domain\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\My.ZhiCore.LanguageManagement.Domain.Shared\\My.ZhiCore.LanguageManagement.Domain.Shared.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.LanguageManagement.Domain.Shared\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\My.ZhiCore.LanguageManagement.EntityFrameworkCore\\My.ZhiCore.LanguageManagement.EntityFrameworkCore.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.LanguageManagement.EntityFrameworkCore\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\My.ZhiCore.LanguageManagement.FreeSqlRepository\\My.ZhiCore.LanguageManagement.FreeSqlRepository.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.FreeSqlRepository\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\My.ZhiCore.LanguageManagement.HttpApi\\My.ZhiCore.LanguageManagement.HttpApi.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.LanguageManagement.HttpApi\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\My.ZhiCore.LanguageManagement.HttpApi.Client\\My.ZhiCore.LanguageManagement.HttpApi.Client.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.LanguageManagement.HttpApi.Client\"/>");
    }

    public static string ReplacePackageReferenceNotificationManagement(this string content)
    {
        return content
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\My.ZhiCore.NotificationManagement.Application\\My.ZhiCore.NotificationManagement.Application.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.NotificationManagement.Application\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\My.ZhiCore.NotificationManagement.Application.Contracts\\My.ZhiCore.NotificationManagement.Application.Contracts.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.NotificationManagement.Application.Contracts\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\My.ZhiCore.NotificationManagement.Domain\\My.ZhiCore.NotificationManagement.Domain.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.NotificationManagement.Domain\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\My.ZhiCore.NotificationManagement.Domain.Shared\\My.ZhiCore.NotificationManagement.Domain.Shared.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.NotificationManagement.Domain.Shared\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\My.ZhiCore.NotificationManagement.EntityFrameworkCore\\My.ZhiCore.NotificationManagement.EntityFrameworkCore.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.NotificationManagement.EntityFrameworkCore\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\My.ZhiCore.NotificationManagement.FreeSqlRepository\\My.ZhiCore.NotificationManagement.FreeSqlRepository.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.FreeSqlRepository\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\My.ZhiCore.NotificationManagement.HttpApi\\My.ZhiCore.NotificationManagement.HttpApi.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.NotificationManagement.HttpApi\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\My.ZhiCore.NotificationManagement.HttpApi.Client\\My.ZhiCore.NotificationManagement.HttpApi.Client.csproj\"/>",
                "<PackageReference Include=\"My.ZhiCore.NotificationManagement.HttpApi.Client\"/>");
    }

    public static string ReplaceMyPackageVersion(this string context, string version)
    {
        return context.Replace("9.0.6.11", version);
    }
}