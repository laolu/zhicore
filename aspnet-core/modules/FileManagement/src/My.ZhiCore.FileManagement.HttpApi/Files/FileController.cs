using Microsoft.AspNetCore.Http;
using Volo.Abp.Content;

namespace My.ZhiCore.FileManagement.Files;

[Route("Files")]
public class FileController : AbpController, IFileAppService
{
    private readonly IFileAppService _fileAppService;

    public FileController(IFileAppService fileAppService)
    {
        _fileAppService = fileAppService;
    }

    [HttpPost("Page")]
    [SwaggerOperation(summary: "分页查询文件", Tags = new[] { "Files" })]
    public async Task<PagedResultDto<PageFileObjectOutput>> PageAsync(PageFileObjectInput input)
    {
        return await _fileAppService.PageAsync(input);
    }

    [HttpPost("Upload")]
    [SwaggerOperation(summary: "上传文件", Tags = new[] { "Files" })]
    public async Task UploadAsync(List<IFormFile> files)
    {
        await _fileAppService.UploadAsync(files);
    }


    [HttpPost("Delete")]
    [SwaggerOperation(summary: "删除文件", Tags = new[] { "Files" })]
    public async Task DeleteAsync(DeleteFileObjectInput input)
    {
        await _fileAppService.DeleteAsync(input);
    }


    [HttpPost("Download")]
    [SwaggerOperation(summary: "下载文件", Tags = new[] { "Files" })]
    public Task<RemoteStreamContent> DownloadAsync(DownloadFileObjectInput input)
    {
        return _fileAppService.DownloadAsync(input);
    }
}