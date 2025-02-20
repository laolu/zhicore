using My.ZhiCore.FileManagement.Files;

namespace My.ZhiCore.FileManagement;

public class FileManagementApplicationAutoMapperProfile : Profile
{
    public FileManagementApplicationAutoMapperProfile()
    {
        CreateMap<Files.FileObjectDto, PageFileObjectOutput>();
    }
}