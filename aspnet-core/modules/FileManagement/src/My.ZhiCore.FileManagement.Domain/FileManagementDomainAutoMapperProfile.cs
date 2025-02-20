using AutoMapper;
using My.ZhiCore.FileManagement.Files;

namespace My.ZhiCore.FileManagement
{
    public class FileManagementDomainAutoMapperProfile : Profile
    {
        public FileManagementDomainAutoMapperProfile()
        {
            CreateMap<FileObject, FileObjectDto>();
        }
    }
}