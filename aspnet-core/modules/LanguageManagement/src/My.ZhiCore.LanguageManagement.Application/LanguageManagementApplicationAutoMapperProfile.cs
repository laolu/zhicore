using My.ZhiCore.LanguageManagement.Languages;
using My.ZhiCore.LanguageManagement.LanguageTexts;

namespace My.ZhiCore.LanguageManagement
{
    public class LanguageManagementApplicationAutoMapperProfile : Profile
    {
        public LanguageManagementApplicationAutoMapperProfile()
        {
            CreateMap<LanguageDto, PageLanguageOutput>();
            CreateMap<Language, PageLanguageOutput>();
            CreateMap<LanguageTextDto, PageLanguageTextOutput>();
        }
    }
}